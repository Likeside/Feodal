using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class ModificationsJSON: JSONData {
        public List<ModificationJSONData> ModificationJsonDatas;
        public override void Load(string path) {
            var jsonText = Resources.Load<TextAsset>(path);
            if(jsonText == null) Debug.Log("ModificationsJSON null");
            Debug.Log(jsonText);
            ModificationJsonDatas = JsonConvert.DeserializeObject<ModificationsJSON>(jsonText.text)?.ModificationJsonDatas;
        }
    }
    
    
    [Serializable]
    public class ModificationJSONData {

        public string type;
        public string name;
        public string numberToModifyName;
        public  string modificationFormula;

        //pending+mission
        public int turnsToComplete;
        public string successChanceFormula;
        public string imageAssetLink;
        public string borderAssetLink;
        
    }
}