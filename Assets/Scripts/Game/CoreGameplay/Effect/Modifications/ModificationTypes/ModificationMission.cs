using System.Collections.Generic;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationMission: ModificationPending {

        public float SuccessChanceValue => _successChanceValue;
        
        readonly string _successChanceFormula;
        float _successChanceValue;

        List<Number> _successChanceFormulaDependencies;

        public ModificationMission(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula, int turnsToComplete, string successChanceFormula) : base(disposable, solver, holder, name, number, modificationFormula, turnsToComplete) {
            _successChanceFormula = successChanceFormula;
            Type = ModificationType.Mission;
        }

        public override void Modify(int turns) {
            Report();
            /*
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            */
            Debug.Log("Turns to complete: " + _turnsToComplete + ", turns: " + turns);
            if(_turnsToComplete - turns != _turnsToComplete) return;
            
            float success = Random.Range(0, 100);
            
            if (success > _successChanceValue) {
                return;
            }
            _number.Value.Value += _modificationValue;
        }

        protected void RecalculateSuccessChance() {
            _successChanceValue = CalculateFormula(_successChanceFormula);
        }

        public override void Initialize() {
            base.Initialize();
            _successChanceValue = CalculateFormula(_successChanceFormula);
            _successChanceFormulaDependencies = GetNumberDependencies(_successChanceFormula);
            SubscribeToDependency(_successChanceFormulaDependencies, RecalculateSuccessChance);
        }
    }
}