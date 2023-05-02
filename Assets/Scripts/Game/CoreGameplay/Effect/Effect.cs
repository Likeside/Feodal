using System.Collections.Generic;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class Effect: DisposableInjection {


        public List<ReactiveProperty<int>> TurnsToCompleteList = new List<ReactiveProperty<int>>();


        readonly List<IModification> _modifications = new List<IModification>();


        public Effect() {
            /*
            TurnsToComplete.Subscribe(
                _ => {
                    foreach (var modification in _modifications) {
                        modification.Modify();
                    }
                }
            ).AddTo(_disposable);

            TurnsToComplete
                .Where(turns => turns == 0)
                .Subscribe(_ => {
                    
                }).AddTo(_disposable);
                */
        }
        
        public void AddModificationBase(Number number, float modification) {
            _modifications.Add(new ModificationBase(number, modification));
        }

        public void AddModificationPending(Number number, float modification, int turnsToComplete) {
            _modifications.Add( new ModificationPending(number, modification, turnsToComplete));
        }

        public void AddModificationMission(Number number, float modification, int turnsToComplete, float successChance) {
            _modifications.Add( new ModificationMission(number, modification, turnsToComplete, successChance));
        }

        public void AddEffect() {
            
        }
        
    }
}