using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    public class JSONData<T>: IJson where T: IJsonData {

        public List<T> jsonDatas;
        public virtual void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            if(jsonText == null) Debug.Log("jsonData null");
            jsonDatas = JsonConvert.DeserializeObject<JSONData<T>>(jsonText.text)?.jsonDatas;
        }

        public void RemoveLastEntity() {
            if(jsonDatas.Any()) jsonDatas.RemoveAt(jsonDatas.Count-1);
        }

        public void AddEntity(IJsonData data) {
            if (data is T jsonData) {
                jsonDatas.Add(jsonData);
            }
        }
    }
}