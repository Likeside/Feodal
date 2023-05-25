using System.Collections.Generic;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using Game.View;
using GameScripts;
using TMPro;
using UnityEngine;

namespace Game {
    public class DummyDisplay: MonoBehaviour {
        [SerializeField] TextMeshProUGUI _numbersText;
        [SerializeField] TextMeshProUGUI _effectsText;
        [SerializeField] TextMeshProUGUI _cardsText;
        [SerializeField] TextMeshProUGUI _eventsText;

        [SerializeField] EffectBlockMb _effectBlockMb;
        [SerializeField] GameObject _effectMbPrefab;



        ObjectPooler _pooler;


        public void Initialize(IDataHolder holder) {
            _pooler = new ObjectPooler(_effectMbPrefab, 20);
            _effectBlockMb.Initialize(EffectType.Unit, _pooler, holder);
        }


        public void DisplayCards(List<Card> cards) {
            _cardsText.text = "";
            foreach (var card in cards) {
                _cardsText.text += card.Name + ":" + card.AvailableCount  + "\n";;
            }
        }

        public void DisplayNumbers(List<Number> numbers) {
            _numbersText.text = "";
            foreach (var number in numbers) {
                _numbersText.text += number.Name + ":" + number.Value + "\n";
            }
        }

        public void DisplayEffects(List<Effect> effects) {
            _effectsText.text = "";
            foreach (var effect in effects) {
                _effectsText.text += effect.Name + ":";

                foreach (var turns in effect.TurnsToCompleteList) {
                    _effectsText.text += turns.Value;
                }
                _effectsText.text += "\n";
            }
            
            
            _effectBlockMb.DisplayEffects(effects);
        }

        public void DisplayEvents(List<RandomEvent> randomEvents) {
            _eventsText.text = "";
            foreach (var randomEvent in randomEvents) {
                _eventsText.text += randomEvent.Name + ":" + randomEvent.EffectCount + "\n";
            }
        }
    }
}