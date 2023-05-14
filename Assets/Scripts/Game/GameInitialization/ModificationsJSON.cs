using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game {
    [Serializable]
    public class ModificationsJSON: JSONData<ModificationJSONData> {
    }
    
    
    [Serializable]
    public class ModificationJSONData: IJsonData {

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