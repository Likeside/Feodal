using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class NumbersJSON: JSONData {
        public List<NumberJSONData> NumberJsonDatas;
        public override void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            NumberJsonDatas = JsonConvert.DeserializeObject<NumbersJSON>(jsonText.text)?.NumberJsonDatas; 
        }
    }

    [Serializable]
    public class NumberJSONData {
       public string name;
       public float initValue;
       public float minValue;
       public float maxValue;
       public string formula;

    }
}