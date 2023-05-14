using System;
using System.Collections.Generic;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationBase: GRES_SolverInjection, IModification {
        
        public string Name { get; }
        protected ModificationType Type { get; set; }
        public float ModificationValue => _modificationValue;

       protected readonly Number _number;
       protected readonly string _modificationFormula;
       protected float _modificationValue;
       
        public ModificationBase(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula) 
            : base(disposable, solver, holder) {
             Name = name;
            _number = number;
            _modificationFormula = modificationFormula;
            Type = ModificationType.Basic;
        }

        public virtual void Modify(int turns) {
            Report();
            _number.Value.Value += _modificationValue;
        }
        
        protected void RecalculateModification() {
            _modificationValue = CalculateFormula(_modificationFormula);
        }

        protected virtual void Report() {
            Debug.Log("Modified: " + Name);
        }

        public virtual void Initialize() {
            _modificationValue = CalculateFormula(_modificationFormula);
            _formulaDependencies = GetNumberDependencies(_modificationFormula);
            SubscribeToDependency(_formulaDependencies, RecalculateModification);
        }



    }
}