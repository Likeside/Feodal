using UniRx;
using Zenject;

namespace Game.CoreGameplay.Effect {
    public class Number: DisposableInjection {
        
        public ReactiveProperty<float> Value = new ReactiveProperty<float>();
        public ReactiveProperty<bool> UnderMinValue = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> OverMaxValue = new ReactiveProperty<bool>();
        
        public string Name { get; }
        
        float _minValue;
        float _maxValue;
        float _initValue;
        
        public Number(string name, float minValue, float maxValue, float initValue) {
            Name = name;
            _minValue = minValue;
            _maxValue = maxValue;
            _initValue = initValue;
            Value.Subscribe(v => {
                CheckIfInBoundaries();
            }).AddTo(_disposable);
            Value.Value = _initValue;
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

        public void SetToInitValue() {
            Value.Value = _initValue;
        }
    }
}