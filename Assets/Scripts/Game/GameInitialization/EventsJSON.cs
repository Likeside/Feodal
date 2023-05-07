using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class EventsJSON {
        public List<EventsJSONData> EventsJsonDatas;
    }

    [Serializable]
    public class EventsJSONData {
        string effectCountNumberName;
        string probabilityNumberName;
    }
}