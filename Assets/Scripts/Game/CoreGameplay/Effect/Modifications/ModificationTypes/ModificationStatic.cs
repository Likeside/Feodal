using Game.CoreGameplay.Injections;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationStatic: ModificationBase {
        public ModificationStatic(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula) 
            : base(disposable, solver, holder, name, number, modificationFormula) {
            Type = ModificationType.Static;
        }

        public override void Modify() {
            Report();
            //TODO: придумать, как не прибавлять одно и тоже на каждом ходу и как перестать учитывать при снятии эффекта
            _number.Value.Value = _modificationValue; //UPD: в формуле задавать зависимости. Т.е. типа "1*количествоЭффектовСолдат + 2*количествоЭффектовВсадник"
        }
    }
}