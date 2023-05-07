using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class EventsJSON: JSONData {
        public List<EventsJSONData> EventsJsonDatas;
        public override void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            EventsJsonDatas = JsonConvert.DeserializeObject<EventsJSON>(jsonText.text)?.EventsJsonDatas;
        }
    }

    [Serializable]
    public class EventsJSONData {
        public string effectCountNumberName;
        public string probabilityNumberName;
    }
}