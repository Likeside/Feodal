using System;
using System.Collections.Generic;
using System.Linq;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game.CoreGameplay.Effect {
    public class Card: GRES_SolverInjection {
        
        public string Name { get; }
        public int AvailableCount => _availableCount;
        public Number _effectCount;
        Dictionary<Number, float> _initCosts;
        Tuple<Number, float> _availability;
        bool _formulaBasedAvailability;
        int _availableCount;

        public Card(Number effectCount, Dictionary<Number, float> initCosts, Tuple<Number, float> availability) : base() {
            _effectCount = effectCount;
            _initCosts = initCosts;
            _availability = availability;

            foreach (var cost in initCosts) {
                cost.Key.Value.Subscribe(_ => {
                    RecalculateAvailability();
                }).AddTo(_disposable);
            }
        }

        void RecalculateAvailability() {
            int baseAvailability = (int)_availability.Item1.Value.Value / (int)_availability.Item2;

            if (baseAvailability <= 0) {
                _availableCount = 0;
                return;
            }
            int resourceAvailability = CalculateResourceAvailability();
            _availableCount = baseAvailability < resourceAvailability ? baseAvailability : resourceAvailability;
        }

        public void ApplyAtEndOfTurn() {
            Debug.Log("Applied card: " + Name);
            _effectCount.Value.Value++;
        }

       public void PayCost() {
           Debug.Log("Paid cost of card: " + Name);
            foreach (var cost in _initCosts) {
                cost.Key.Value.Value -= cost.Value;
            }
        }

       public void RestoreCostBeforeEndOfTurn() {
           Debug.Log("Restored cost of card: " + Name);
            foreach (var cost in _initCosts) {
                cost.Key.Value.Value += cost.Value;
            }
        }
        
        

        int CalculateResourceAvailability() {
            return _initCosts.Min(c => (int) c.Key.Value.Value / (int) c.Value);
        }
    }
}