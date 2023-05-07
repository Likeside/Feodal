using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Game {
    [Serializable]
    public class CardsJSON: JSONData {
        [JsonProperty("CardJsonDatas")]
        public List<CardJSONData> CardJsonDatas;

        public override void Load(string path) {
            throw new NotImplementedException();
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