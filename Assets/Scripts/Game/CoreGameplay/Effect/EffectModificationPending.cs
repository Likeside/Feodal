using UniRx;

namespace Game.CoreGameplay.Effect {
    public class EffectModificationPending: EffectModificationBase {

        public ReactiveProperty<int> TurnsToComplete = new ReactiveProperty<int>();
        
        public EffectModificationPending(Number number, float modification, int turnsToComplete) : base(number, modification) {
            TurnsToComplete.Value = turnsToComplete;
        }

        public override void Modify() {
            if (TurnsToComplete.Value > 0) {
                TurnsToComplete.Value--;
                return;
            }
            base.Modify();
        }
    }
}