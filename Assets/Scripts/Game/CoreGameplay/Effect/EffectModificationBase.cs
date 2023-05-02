namespace Game.CoreGameplay.Effect {
    public class EffectModificationBase {
        
       protected Number _number;
       protected float _modification; 

        
        public EffectModificationBase(Number number, float modification) {
            _number = number;
            _modification = modification;
        }

        public virtual void Modify() {
            _number.Value.Value += _modification;
        }

    }
}