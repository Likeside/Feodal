using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class EventsJSON: JSONData<EventsJSONData> {

    }

    [Serializable]
    public class EventsJSONData {
        public string effectCountNumberName;
        public string probabilityNumberName;
    }
}