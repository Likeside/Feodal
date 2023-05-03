using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationMission: ModificationPending {
        
        readonly string _successChanceFormula;
        readonly float _successChanceValue;
        
        public ModificationMission(string name, Number number, string modificationFormula, int turnsToComplete, string successChanceFormula) : base(name, number, modificationFormula, turnsToComplete) {
            _successChanceFormula = successChanceFormula;
            Type = ModificationType.Mission;
        }

        public override void Modify() {
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            float success = Random.Range(0, 100);
            
            if (success < _successChanceValue) {
                return;
            }
            
            _number.Value.Value += _modificationValue;
        }
    }
}