using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class NumbersJSON: JSONData<NumberJSONData> {
    }

    [Serializable]
    public class NumberJSONData: IJsonData {
       public string name;
       public float initValue;
       public float minValue;
       public float maxValue;
       public string formula;

    }
}