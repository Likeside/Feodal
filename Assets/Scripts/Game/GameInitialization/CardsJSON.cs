using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class CardsJSON: JSONData {
        [JsonProperty("CardJsonDatas")]
        public List<CardJSONData> CardJsonDatas;

        public override void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            if(jsonText == null) Debug.Log("CardsJSON null");
            CardJsonDatas = JsonConvert.DeserializeObject<CardsJSON>(jsonText.text)?.CardJsonDatas;
        }
    }

    [Serializable]
    public class CardJSONData {
        public string effectCountNumberName;
        public  Dictionary<string, float> initCosts;
        public string availabilityNumberName;
        public float availabilityParameter;
    }
}