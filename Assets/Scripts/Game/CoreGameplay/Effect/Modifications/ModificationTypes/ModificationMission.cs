using System.Collections.Generic;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationMission: ModificationPending {
        
        readonly string _successChanceFormula;
        float _successChanceValue;

        List<Number> _successChanceFormulaDependencies;

        public ModificationMission(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula, int turnsToComplete, string successChanceFormula) : base(disposable, solver, holder, name, number, modificationFormula, turnsToComplete) {
            _successChanceFormula = successChanceFormula;
            _successChanceValue = CalculateFormula(successChanceFormula);
            _successChanceFormulaDependencies = GetNumberDependencies(successChanceFormula);
            SubscribeToDependency(_successChanceFormulaDependencies, RecalculateSuccessChance);
            Type = ModificationType.Mission;
        }

        public override void Modify() {
            Report();
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            float success = Random.Range(0, 100);
            
            if (success < _successChanceValue) {
                return;
            }
            _number.Value.Value += _modificationValue;
        }

        protected void RecalculateSuccessChance() {
            _successChanceValue = CalculateFormula(_successChanceFormula);
        }
    }
}