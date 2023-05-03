namespace Game.CoreGameplay.Effect {
    public class ModificationBase: IModification {
        
        protected string Name { get; private set; }

       protected readonly Number _number;
       protected readonly string _modification; 

        
        public ModificationBase(string name, Number number, string modification) {
            Name = name;
            _number = number;
            _modification = modification;
        }

        public virtual void Modify() {
          //  _number.Value.Value += _modification;
        }

    }
}