using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class ModificationsJSON: JSONData {
        public List<ModificationJSONData> ModificationJsonDatas;
        public override void Load(string path) {
            throw new NotImplementedException();
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