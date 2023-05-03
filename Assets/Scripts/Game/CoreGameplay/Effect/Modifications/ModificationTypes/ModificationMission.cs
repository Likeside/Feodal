using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationMission: ModificationPending {
        
        readonly float _successChance;
        
        public ModificationMission(string name, Number number, string modification, int turnsToComplete, float successChance) : base(name, number, modification, turnsToComplete) {
            _successChance = successChance;
        }

        public override void Modify() {
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            float success = Random.Range(0, 100);
            if (success < _successChance) {
                return;
            }
          //  _number.Value.Value += _modification;
        }
    }
}