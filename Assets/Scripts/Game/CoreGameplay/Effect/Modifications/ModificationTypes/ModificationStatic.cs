using System;
using System.Linq;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class ModificationStatic: ModificationBase {
        public ModificationStatic(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula) 
            : base(disposable, solver, holder, name, number, modificationFormula) {
            Type = ModificationType.Static;
        }

        public override void Modify(int turns) {
            Report();
            RecalculateModification();
            //TODO: придумать, как не прибавлять одно и тоже на каждом ходу и как перестать учитывать при снятии эффекта
            Debug.Log("Static dummy mod value: " + _modificationValue);
            _number.Value.Value = _modificationValue; //UPD: в формуле задавать зависимости. Т.е. типа "1*количествоЭффектовСолдат + 2*количествоЭффектовВсадник"
        }

        protected override void Report() {
            base.Report();
            Debug.Log($"Modification: {Name} depends on: " + String.Join(",", _formulaDependencies.Select(_ => _.Name)));
        }
    }
}