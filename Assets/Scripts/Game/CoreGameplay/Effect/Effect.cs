using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class Effect: DisposableInjection {
        
        
        
        public readonly List<ReactiveProperty<int>> TurnsToCompleteList = new List<ReactiveProperty<int>>();
        readonly List<IModification> _modifications = new List<IModification>();
        
        public Effect() {
       
        }
        public void ApplyEffect() {
            foreach (var turns in TurnsToCompleteList) {
                if (turns.Value > 0) {
                    turns.Value--; //если изначальное количество ходов до окончания эффекта задать отрицательным, то эффект будет постоянным
                }
            }
        }
        public void AddEffect(int turns) {
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
        public void RemoveFirstEffect() {
            TurnsToCompleteList.RemoveAt(0);
        }
        public void ClearEffects() {
            TurnsToCompleteList.Clear();
        }

        //методы для эдитора для добавления модификаций в эффект. нужно придумать, как сериализовать
        public void AddModificationBase(Number number, float modification) {
            _modifications.Add(new ModificationBase(number, modification));
        }

        public void AddModificationPending(Number number, float modification, int turnsToComplete) {
            _modifications.Add( new ModificationPending(number, modification, turnsToComplete));
        }

        public void AddModificationMission(Number number, float modification, int turnsToComplete, float successChance) {
            _modifications.Add( new ModificationMission(number, modification, turnsToComplete, successChance));
        }


        
    }
}