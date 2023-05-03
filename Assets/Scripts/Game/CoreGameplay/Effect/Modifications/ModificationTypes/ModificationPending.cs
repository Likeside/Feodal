using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationPending: ModificationBase {

        protected int _turnsToComplete;
        
        public ModificationPending(string name, Number number, string modificationFormula, int turnsToComplete) : base(name, number, modificationFormula) {
            _turnsToComplete = turnsToComplete;
            Type = ModificationType.Pending;
        }

        public override void Modify() {
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            base.Modify();
        }
    }
}