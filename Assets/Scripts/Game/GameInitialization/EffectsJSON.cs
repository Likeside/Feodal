using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class EffectsJSON {
        public List<EffectJSONData> EffectJsonDatas;
    }

    [Serializable]
    public class EffectJSONData {

        string type;
        string countNumberName;
        List<string> modificationsName;
        int initTurns;
        string turnModificationNumberName;
        string description;
        string subdescription;
        string imageAssetLink;
        string borderAssetLink;
        
    }
}