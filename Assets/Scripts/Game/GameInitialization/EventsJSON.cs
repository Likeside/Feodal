using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class EventsJSON: JSONData {
        public List<EventsJSONData> EventsJsonDatas;
        public override void Load(string path) {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class EventsJSONData {
        public string effectCountNumberName;
        public string probabilityNumberName;
    }
}