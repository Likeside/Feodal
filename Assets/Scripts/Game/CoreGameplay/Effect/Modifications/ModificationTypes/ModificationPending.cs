using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationPending: ModificationBase {

        protected int _turnsToComplete;
        
        public ModificationPending(string name, Number number, string modification, int turnsToComplete) : base(name, number, modification) {
            _turnsToComplete = turnsToComplete;
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