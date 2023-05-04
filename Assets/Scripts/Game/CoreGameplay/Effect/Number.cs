using System;
using System.Collections.Generic;
using Game.CoreGameplay.Injections;
using UniRx;
using Zenject;

namespace Game.CoreGameplay.Effect {
    public class Number: GRES_SolverInjection {
        
        public ReactiveProperty<float> Value = new ReactiveProperty<float>();
        public ReactiveProperty<bool> UnderMinValue = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> OverMaxValue = new ReactiveProperty<bool>();
        
        public string Name { get; }
        
        float _minValue;
        float _maxValue;
        float _initValue;
        string _formula;
        

        public Number(string name, float initValue, float minValue = float.MinValue, float maxValue = float.MaxValue, string formula = "") {
            Name = name;
            _minValue = minValue;
            _maxValue = maxValue;
            _initValue = initValue;
            Value.Subscribe(v => {
                CheckIfInBoundaries();
            }).AddTo(_disposable);
            Value.Value = _initValue;

            if (formula != String.Empty) {
                _formula = formula;
                _formulaDependencies = GetNumberDependencies(formula);
                SubscribeToDependency(_formulaDependencies, CalculateValue);
                CalculateValue();
            }
        }

        void CheckIfInBoundaries() {
            UnderMinValue.Value = Value.Value < _minValue;
            OverMaxValue.Value = Value.Value > _maxValue;
            SetToCorrectValue();
        }
        
        protected virtual void SetToCorrectValue() {
            if (UnderMinValue.Value) Value.Value = _minValue;
            if (OverMaxValue.Value) Value.Value = _maxValue;
        }

        protected void CalculateValue() {
            Value.Value = CalculateFormula(_formula);
        }

        public void SetToInitValue() {
            Value.Value = _initValue;
        }
    }
}