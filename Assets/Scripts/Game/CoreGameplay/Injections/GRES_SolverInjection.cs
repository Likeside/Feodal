using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class GRES_SolverInjection {

        protected GRES_Solver _solver;
        protected List<string> _passedVariables;
        protected INumbersValueHolder _numbersValueHolder;
        string _symbol;

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
            
            if (formula.Contains(_symbol)) {
                var matches = Regex.Matches(formula, @"(?<={_symbol})\w+(?={_symbol})");
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

            string filteredFormula = formula.Replace(_symbol, "");
            foreach (var variable in variables) {
               filteredFormula = filteredFormula.Replace(variable, _numbersValueHolder.GetNumberValue(variable).ToString(CultureInfo.InvariantCulture));
            }
            
            _solver.SetExpression(filteredFormula);
            _solver.Prepare();
            return _solver.Evaluate();
        }
    }
}