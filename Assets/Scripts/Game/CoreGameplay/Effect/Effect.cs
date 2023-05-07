using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class Effect: DisposableInjection {
        public string Name { get; }
        public List<string> ModificationsName { get; }
        public ReactiveCollection<ReactiveProperty<int>> TurnsToCompleteList = new();
        readonly List<IModification> _modifications;
        int _initTurns;
        ReactiveProperty<int> _currentTurnsToRemove;
        Number _turnModificatorNumber;
        Number _effectCount;
        int _previousCountValue;
        
        public Effect(Number effectCount, List<IModification> modifications, string name, int initTurns, Number turnModificatorNumber) {
            _modifications = modifications;
            Name = name;
            _initTurns = initTurns;
            _turnModificatorNumber = turnModificatorNumber;
            ModificationsName = _modifications.Select(m => m.Name).ToList();
            
            _effectCount = effectCount;
            _previousCountValue = (int)_effectCount.Value.Value;
            _effectCount.Value.Subscribe(AddOrRemoveEffect).AddTo(_disposable);
        }

        public void Load(ReactiveCollection<ReactiveProperty<int>> turnsToCompleteList) {
            TurnsToCompleteList = turnsToCompleteList;
            foreach (var turnsProperty in turnsToCompleteList) {
               SubscribeModificationToTurns(turnsProperty);
            }
        }
        
        public void ApplyAtEndOfTurn() {
            Debug.Log("Applied effect: " + Name);
            foreach (var turns in TurnsToCompleteList) {
                if (turns.Value > 0) {
                    turns.Value--; //если изначальное количество ходов до окончания эффекта задать отрицательным, то эффект будет постоянным
                }
            }
        }
        
        //вызывать этот метод при активации эффекта (карта, событие или модификация может вызвать активацию)
        //UPD: метод будет вызываться при смене value в effectCount, если он увеличен
        void Add() {
            Debug.Log("Added effect: " + Name);
            var turns = _initTurns + (int)_turnModificatorNumber.Value.Value;
            var turnsProperty = new ReactiveProperty<int>(turns);
            SubscribeModificationToTurns(turnsProperty);
           //добавляем количество ходов до окончания эффекта в список количества ходов/количества эффектов
            TurnsToCompleteList.Add(turnsProperty);
        }

        void SubscribeModificationToTurns(ReactiveProperty<int> turnsProperty) {
            //на каждое изменение количества ходов вызываем модификацию
            turnsProperty.Subscribe(
                _ => {
                    foreach (var modification in _modifications) {
                        modification.Modify();
                    }
                }
            ).AddTo(_disposable);
            
            //если количество оставшихся ходов равно нулю, удаляем свойство из списка, чтобы не вызывать модификации
            turnsProperty.Where(t => t == 0)
                .Subscribe(_ => {
                    _currentTurnsToRemove = turnsProperty; //выставляем конкретное проперти ходов, чтобы именно оно отремувилось
                    _effectCount.Value.Value--;
                }).AddTo(_disposable);
        }

        void RemoveFirst() {
            _disposable.Remove(TurnsToCompleteList[0]);
            TurnsToCompleteList.RemoveAt(0);
        } 
        
        void Clear() {
            foreach (var property in TurnsToCompleteList) {
                _disposable.Remove(property);
            }
            TurnsToCompleteList.Clear();
        }

        void AddOrRemoveEffect(float count) {
            if (count > _previousCountValue) {
                int times = (int)count - _previousCountValue;
                for (int i = 0; i < times; i++) {
                    Add();
                }
            }
            else {
                Debug.Log("Removed effect: " + Name);
                int times = _previousCountValue - (int)count;

                for (int i = 0; i < times; i++) {
                    if (_currentTurnsToRemove != null) { //если конкретное проперти ходов не выставлено, просто самый первый эффект снимаем
                        _disposable.Remove(_currentTurnsToRemove);
                        TurnsToCompleteList.Remove(_currentTurnsToRemove);
                        _currentTurnsToRemove = null;
                    }
                    else {
                        RemoveFirst();
                    }
                }
            }
            _previousCountValue = (int)count;
        }
    }
}