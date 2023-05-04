using Game.CoreGameplay.Effect;
using UnityEngine;

namespace Game.CoreGameplay.Injections {
    public interface IViewDataHolder {
        
        public Sprite GetModificationIconByName(string modificationName);

        public Sprite GetModificationBorderByName(string modificationName);

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