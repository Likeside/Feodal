using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class NumbersJSON: JSONData {
        public List<NumberJSONData> NumberJsonDatas;
        public override void Load(string path) {
            throw new NotImplementedException();
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