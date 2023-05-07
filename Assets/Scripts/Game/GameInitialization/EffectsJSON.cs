using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class EffectsJSON: JSONData {
        public List<EffectJSONData> EffectJsonDatas;
        public override void Load(string path) {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class EffectJSONData {

        public string type;
        public  string countNumberName;
        public List<string> modificationsName;
        public int initTurns;
        public  string turnModificationNumberName;
        public string description;
        public string subdescription;
        public string imageAssetLink;
        public string borderAssetLink;
        
    }
}