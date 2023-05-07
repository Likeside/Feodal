using System.Collections.Generic;
using System.Linq;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using UnityEngine;

namespace Game {
    public class Holder: IDataHolder {

        public List<Number> Numbers => _numbers;

        public List<Effect> Effects => _effects;
        public List<Card> Cards => _cards;
        public List<RandomEvent> RandomEvents => _events;

        List<Number> _numbers;
        List<ModificationBase> _modifications;
        List<Effect> _effects;
        List<Card> _cards;
        List<RandomEvent> _events;
        
        List<NumberJSONData> _numberJsonDatas;
        List<ModificationJSONData> _modificationJsonDatas;
        List<EffectJSONData> _effectJsonDatas;
        List<CardJSONData> _cardJsonDatas;
        List<EventsJSONData> _eventsJsonDatas;
        
        public void PassNumbers(List<Number> numbers, List<NumberJSONData> numberJsonDatas) {
            _numbers = numbers;
            _numberJsonDatas = numberJsonDatas;

        }
        public void PassModifications(List<ModificationBase> modifications, List<ModificationJSONData> modificationJsonDatas) {
            _modifications = modifications;
            _modificationJsonDatas = modificationJsonDatas;
        }
        public void PassEffects(List<Effect> effects, List<EffectJSONData> effectJsonDatas) {
            _effects = effects;
            _effectJsonDatas = effectJsonDatas;
        }
        public void PassCards(List<Card> cards, List<CardJSONData> cardJsonDatas) {
            _cards = cards;
            _cardJsonDatas = cardJsonDatas;
        }
        public void PassEvent(List<RandomEvent> events, List<EventsJSONData> eventsJsonDatas) {
            _events = events;
            _eventsJsonDatas = eventsJsonDatas;
        }
        
        public float GetNumberValue(string numberName) {
                var number = _numbers.FirstOrDefault(n => n.Name.Equals(numberName));
                if (number != null) {
                    return number.Value.Value;
                }
                Debug.LogError("Passed incorrect numberName: " + numberName);
                return 0f;
        }

        public Number GetNumber(string numberName) {
            var number = _numbers.FirstOrDefault(n => n.Name.Equals(numberName));
            if (number != null) {
                return number;
            }
            Debug.LogError("Passed incorrect numberName: " + numberName);
            return _numbers[0];
        }

        public IModification GetModificationByName(string modificationName) {
            var mod = _modifications.FirstOrDefault(m => m.Name.Equals(modificationName));
            if (mod != null) {
                return mod;
            }
            Debug.LogError("Passed incorrect modName: " + modificationName);
            return _modifications[0];
        }

        public Sprite GetModificationIconByName(string modificationName) {
            throw new System.NotImplementedException();
        }

        public Sprite GetModificationBorderByName(string modificationName) {
            throw new System.NotImplementedException();
        }

        public float GetModificationValueByName(string modificationName) {
            throw new System.NotImplementedException();
        }

        public Sprite GetEffectIconByName(string effectName) {
            throw new System.NotImplementedException();
        }

        public Sprite EffectBorderByName(string effectName) {
            throw new System.NotImplementedException();
        }

        public Sprite GetCardIconByName(string cardName) {
            throw new System.NotImplementedException();
        }

        public Sprite GetCardBorderByName(string cardName) {
            throw new System.NotImplementedException();
        }

        public string GetEffectDescriptionByName(string effectName) {
            throw new System.NotImplementedException();
        }

        public string GetEffectSubdescriptionByName(string effectName) {
            throw new System.NotImplementedException();
        }

        public EffectType GetEffectTypeByName(string effectName) {
            throw new System.NotImplementedException();
        }

        public string GetCardDescriptionByName(string cardName) {
            throw new System.NotImplementedException();
        }
    }
}