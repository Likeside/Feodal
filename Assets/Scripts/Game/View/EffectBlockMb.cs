using System.Collections.Generic;
using System.Linq;
using Game.CoreGameplay.Effect;
using UnityEngine;

namespace Game.View {
    public class EffectBlockMb: MonoBehaviour {
        EffectType _type;

        List<Effect> _effectsToDisplay;


        List<EffectMb> _currentEffectsMb;


        public void DisplayEffects(List<Effect> allEffects) {
            _effectsToDisplay = allEffects.Where(e => e.Type == _type && e.TurnsToCompleteList.Count > 0)
                .OrderBy(_ => _.Name).ToList();
            
            
            
            
            
            
        }
        
    }
}