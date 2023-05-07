using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class CardsJSON {
        public List<CardJSONData> CardJsonDatas;
    }

    [Serializable]
    public class CardJSONData {
        string effectCountNumberName;
        Dictionary<string, float> initCosts;
        string availabilityNumberName;
        float availabilityParameter;
    }
}