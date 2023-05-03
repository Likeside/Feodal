using System;
using System.Collections.Generic;
using Game.CoreGameplay.Injections;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationBase: GRES_SolverInjection, IModification {
        
        public string Name { get; }
        protected ModificationType Type { get; set; }

       protected readonly Number _number;
       protected readonly string _modificationFormula;
       protected float _modificationValue;

       List<Number> _formulaDependencies;


        public ModificationBase(string name, Number number, string modificationFormula) {
             Name = name;
            _number = number;
            _modificationFormula = modificationFormula;
            _modificationValue = CalculateFormula(modificationFormula);
            _formulaDependencies = GetNumberDependencies(modificationFormula);
            SubscribeToDependency(_formulaDependencies, RecalculateModification);
            Type = ModificationType.Basic;
        }

        public virtual void Modify() {
            _number.Value.Value += _modificationValue;
        }

        protected List<Number> GetNumberDependencies(string formula) {
            var numberDependencies = new List<Number>();
            foreach (var variable in GetVariablesFromString(formula)) {
                numberDependencies.Add(_numbersValueHolder.GetNumber(variable));
            }
            return numberDependencies;
        }

        protected void RecalculateModification() {
            _modificationValue = CalculateFormula(_modificationFormula);
        }

        protected void SubscribeToDependency(List<Number> dependencies, Action subscription) {
            foreach (var numberDependency in dependencies) {
                numberDependency.Value.Subscribe(_ => {
                    subscription?.Invoke();
                }).AddTo(_disposable);
            } 
        }

    }
}