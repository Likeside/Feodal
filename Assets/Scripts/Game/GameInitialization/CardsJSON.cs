using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class CardsJSON: JSONData<CardJSONData> {

    }

    [Serializable]
    public class CardJSONData {
        public string effectCountNumberName;
        public  Dictionary<string, float> initCosts;
        public string availabilityNumberName;
        public float availabilityParameter;
    }
}