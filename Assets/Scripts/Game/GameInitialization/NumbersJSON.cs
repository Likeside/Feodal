using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class NumbersJSON {
        public List<NumberJSONData> NumberJsonDatas;
    }

    [Serializable]
    public class NumberJSONData {
        string name;
        float initValue;
        float minValue;
        float maxValue;
        string formula;

    }
}