using UniRx;

namespace Game.CoreGameplay.Effect {
    public class Number {
        public ReactiveProperty<float> Value = new ReactiveProperty<float>();
    }
}