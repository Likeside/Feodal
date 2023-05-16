using Game.CoreGameplay.Injections;
using UniRx;

namespace Game.CoreGameplay.Effect {
    public class ModificationPending: ModificationBase {

        protected int _turnsToComplete;
        
        public ModificationPending(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder, string name, Number number, string modificationFormula, int turnsToComplete) :
            base(disposable, solver, holder, name, number, modificationFormula) {
            _turnsToComplete = turnsToComplete;
            Type = ModificationType.Pending;
        }

        public override void Modify(int turns) {
            /*
            if (_turnsToComplete > 0) {
                _turnsToComplete--;
                return;
            }
            */
            if(_turnsToComplete - turns != _turnsToComplete) return;

            base.Modify(turns);
        }
    }
}