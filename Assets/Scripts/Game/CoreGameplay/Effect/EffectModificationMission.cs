using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class EffectModificationMission: EffectModificationPending {

        float _successChance;
        
        public EffectModificationMission(Number number, float modification, int turnsToComplete, float successChance) : base(number, modification, turnsToComplete) {
            _successChance = successChance;
        }

        public override void Modify() {
            if (TurnsToComplete.Value > 0) {
                TurnsToComplete.Value--;
                return;
            }
            float success = Random.Range(0, 100);
            if (success < _successChance) {
                return;
            }
            _number.Value.Value += _modification;
        }
    }
}