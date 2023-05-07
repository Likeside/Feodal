using System.Collections.Generic;
using Game.CoreGameplay.Effect;
using UnityEngine;

namespace Game.CoreGameplay.Injections {
    public interface IDataHolder {
        
        public void PassNumbers(List<Number> numbers, List<NumberJSONData> numberJsonDatas);

        public void PassModifications(List<ModificationBase> modifications, List<ModificationJSONData> modificationJsonDatas);

        public void PassEffects(List<Effect.Effect> effects, List<EffectJSONData> effectJsonDatas);

        public void PassCards(List<Card> cards, List<CardJSONData> cardJsonDatas);

        public void PassEvent(List<RandomEvent> events, List<EventsJSONData> eventsJsonDatas);
        
        public float GetNumberValue(string numberName);

        public Number GetNumber(string numberName);

        public IModification GetModificationByName(string modificationName);
        public Sprite GetModificationIconByName(string modificationName);

        public Sprite GetModificationBorderByName(string modificationName);

        public float GetModificationValueByName(string modificationName);

        public Sprite GetEffectIconByName(string effectName);

        public Sprite EffectBorderByName(string effectName);

        public Sprite GetCardIconByName(string cardName);

        public Sprite GetCardBorderByName(string cardName);

        public string GetEffectDescriptionByName(string effectName);
        public string GetEffectSubdescriptionByName(string effectName);

        public EffectType GetEffectTypeByName(string effectName);
        
        public string GetCardDescriptionByName(string cardName);

    }
}