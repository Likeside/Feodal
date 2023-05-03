using System.Collections.Generic;
using Game.CoreGameplay.Injections;

namespace Game.CoreGameplay.Effect {
    public class ModificationBase: GRES_SolverInjection, IModification {
        
        protected string Name { get; private set; }
        protected ModificationType Type { get; set; }

       protected readonly Number _number;
       protected readonly string _modificationFormula;
       protected readonly float _modificationValue;

       List<Number> _formulaDependencies;


        public ModificationBase(string name, Number number, string modificationFormula) {
             Name = name;
            _number = number;
            _modificationFormula = modificationFormula;
            _modificationValue = CalculateFormula(modificationFormula);
            _formulaDependencies = new List<Number>();
            foreach (var variable in GetVariablesFromString(modificationFormula)) {
                _formulaDependencies.Add(_numbersValueHolder.GetNumber(variable));
            }

            Type = ModificationType.Basic;
        }

        public virtual void Modify() {
            _number.Value.Value += _modificationValue;
        }

    }
}