using System.Collections.Generic;

namespace Game.CoreGameplay.Effect {
    public class Card {
        public string Name { get; }
        public Effect _effect;

        Dictionary<Number, string> _initCosts;


        Dictionary<string, float> _availabilityFormulas;


    }
}