namespace Game.CoreGameplay.Effect {
    public class ModificationBase: IModification {
        
       protected readonly Number _number;
       protected readonly float _modification; 

        
        public ModificationBase(Number number, float modification) {
            _number = number;
            _modification = modification;
        }

        public virtual void Modify() {
            _number.Value.Value += _modification;
        }

    }
}