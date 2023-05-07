using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.CoreGameplay.Effect {
    public class RandomEvent {
        public string Name { get; }
        
        Number _probability;
        Number _effectCount;

        public RandomEvent(string name, Number effectCount, Number probability) {
            Name = name;
            _effectCount = effectCount;
            _probability = probability;
        }

        public void TryToApplyEvent() {
            if (Random.Range(0, 100) > _probability.Value.Value) {
                _effectCount.Value.Value++;
            }
        }
    }
}