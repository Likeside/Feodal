using System.Collections.Generic;
using System.Linq;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class EffectPanelView: DisposableMonoInjection {
        List<Effect> _effects;


        void Start() {
            foreach (var effect in _effects) {
                effect.TurnsToCompleteList.ObserveAdd().Subscribe(_ => {
                    
                }).AddTo(_disposable);
            }
        }


        void EffectActivated() {
            
        }


        void RefreshEffectDisplay(Effect effect) {
            //надо придумать, как поменьше аллоцировать
            var distinct = effect.TurnsToCompleteList.Distinct();
            var dictionary = new Dictionary<int, int>();
            foreach (var dist in distinct) {
                dictionary.Add(dist.Value, 0);
            }
            foreach (var t in effect.TurnsToCompleteList) {
                if (dictionary.Keys.Any(k => k == t.Value)) {
                    dictionary[t.Value]++;
                }
            }
            DisplayEffectsGroup(effect, dictionary);
        }

        void DisplayEffectsGroup(Effect effect, Dictionary<int, int> dictionary) {
            //брать вьюшки из пула
        }
    }
}