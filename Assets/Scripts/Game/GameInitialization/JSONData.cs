using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    public class JSONData<T> {

        public List<T> jsonDatas;
        public virtual void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            if(jsonText == null) Debug.Log("jsonData null");
            jsonDatas = JsonConvert.DeserializeObject<JSONData<T>>(jsonText.text)?.jsonDatas;
        }
        
    }
}