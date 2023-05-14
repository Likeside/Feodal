using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class EffectsJSON: JSONData<EffectJSONData> {

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