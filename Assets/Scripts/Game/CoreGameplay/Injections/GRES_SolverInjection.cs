using System.Collections.Generic;
using System.Text.RegularExpressions;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class GRES_SolverInjection {

        protected GRES_Solver _solver;
        protected List<string> _passedVariables;
        protected INumbersValueHolder _numbersValueHolder;

        [Inject]
        public void SetDisposable(GRES_Solver solver) {
            _solver = solver;
        }
        [Inject]
        public void SetNumbersValueHolder(INumbersValueHolder numbersValueHolder) {
            _numbersValueHolder = numbersValueHolder;
        }

        List<string> GetVariablesFromString(string formula) {
            _passedVariables ??= new List<string>();
            _passedVariables.Clear();
            
            if (formula.Contains("")) {
                var matches = Regex.Matches(formula, @"(?<=Q)\w+(?=Q)");
                foreach (Match match in matches)
                    _passedVariables.Add(match.Value);

            }
            return _passedVariables;
        }

        float CalculateFormula(string formula) {
            var variables = GetVariablesFromString(formula);

            if (variables.Count == 0) {
                _solver.SetExpression(formula);
                _solver.Prepare();
                return _solver.Evaluate();
            }

            return 0;
        }
    }
}