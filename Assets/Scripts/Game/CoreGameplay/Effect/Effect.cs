using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class Effect: DisposableInjection {
        public string Name { get; }
        public List<string> ModificationsName { get; }
        public readonly ReactiveCollection<ReactiveProperty<int>> TurnsToCompleteList = new();
        readonly List<IModification> _modifications;
        int _initTurns;
        Number _turnModificatorNumber;
        
        public Effect(List<IModification> modifications, string name, int initTurns, Number turnModificatorNumber) {
            _modifications = modifications;
            Name = name;
            _initTurns = initTurns;
            _turnModificatorNumber = turnModificatorNumber;
            ModificationsName = _modifications.Select(m => m.Name).ToList();
        }
        
        public void ApplyAtEndOfTurn() {
            foreach (var turns in TurnsToCompleteList) {
                if (turns.Value > 0) {
                    turns.Value--; //если изначальное количество ходов до окончания эффекта задать отрицательным, то эффект будет постоянным
                }
            }
        }
        
        //вызывать этот метод при активации эффекта
        public void Add() {
            var turns = _initTurns + (int)_turnModificatorNumber.Value.Value;
            var turnsProperty = new ReactiveProperty<int>(turns);
            
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
                    TurnsToCompleteList.Remove(turnsProperty);
                }).AddTo(_disposable);
            
            //добавляем количество ходов до окончания эффекта в список количества ходов/количества эффектов
            TurnsToCompleteList.Add(turnsProperty);
        }
        public void RemoveFirst() {
            _disposable.Remove(TurnsToCompleteList[0]);
            TurnsToCompleteList.RemoveAt(0);
        }
        public void Clear() {
            foreach (var property in TurnsToCompleteList) {
                _disposable.Remove(property);
            }
            TurnsToCompleteList.Clear();
        }
    }
}