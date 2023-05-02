using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationPending: ModificationBase {

        protected int _turnsToComplete;
        
        public ModificationPending(Number number, float modification, int turnsToComplete) : base(number, modification) {
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