using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class EffectsJSON: JSONData {
        public List<EffectJSONData> EffectJsonDatas;
        public override void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            if(jsonText == null) Debug.Log("EffectsJSON null");
            EffectJsonDatas = JsonConvert.DeserializeObject<EffectsJSON>(jsonText.text)?.EffectJsonDatas;
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