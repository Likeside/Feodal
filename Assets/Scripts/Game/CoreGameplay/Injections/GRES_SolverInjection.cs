using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Game.CoreGameplay.Effect;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class GRES_SolverInjection: DisposableInjection {

        protected GRES_Solver _solver;
        protected List<string> _passedVariables;
        protected IDataHolder _numbersValueHolder;
        protected string _startSymbol = "<";
        protected string _endSymbol = ">";

        protected List<Number> _formulaDependencies;
        
        [Inject]
        public void SetDisposable(GRES_Solver solver) {
            _solver = solver;
        }
        [Inject]
        public void SetNumbersValueHolder(IDataHolder numbersValueHolder) {
            _numbersValueHolder = numbersValueHolder;
        }

        protected List<string> GetVariablesFromString(string formula) {
            _passedVariables ??= new List<string>();
            _passedVariables.Clear();
            
            //TODO: ЗАМЕНИТЬ СПОСОБ ВЫЧЛЕНЕНИЯ ВАРАЕБЛОВ
            if (formula.Contains(_startSymbol)) {
                var matches = Regex.Matches(formula, $"{Regex.Escape(_startSymbol)}(.*?){Regex.Escape(_endSymbol)}"); // @"(?<={_symbol})\w+(?={_symbol})");
                foreach (Match match in matches) {
                    _passedVariables.Add(match.Groups[1].Value);
                    Debug.Log("Added variable: " + match.Groups[1].Value);
                }

            }
            ////
            return _passedVariables;
        }

        protected float CalculateFormula(string formula) {
            var variables = GetVariablesFromString(formula);
            
            if (variables.Count == 0) {
                _solver.SetExpression(formula);
                _solver.Prepare();
                return _solver.Evaluate();
            }

            string filteredFormula = formula.Replace(_startSymbol, "");
            filteredFormula = filteredFormula.Replace(_endSymbol, "");
            foreach (var variable in variables) {
               filteredFormula = filteredFormula.Replace(variable, _numbersValueHolder.GetNumberValue(variable).ToString(CultureInfo.InvariantCulture));
            }
            
            _solver.SetExpression(filteredFormula);
            _solver.Prepare();
            return _solver.Evaluate();
        }
        
        protected List<Number> GetNumberDependencies(string formula) {
            if (!formula.Contains(_startSymbol)) return null;
            var numberDependencies = new List<Number>();
            foreach (var variable in GetVariablesFromString(formula)) {
                numberDependencies.Add(_numbersValueHolder.GetNumber(variable));
            }
            return numberDependencies;
        }
        
        protected void SubscribeToDependency(List<Number> dependencies, Action subscription) {
            if(dependencies == null) return;
            foreach (var numberDependency in dependencies) {
                numberDependency.Value.Subscribe(_ => {
                    subscription?.Invoke();
                }).AddTo(_disposable);
            } 
        }
    }
}