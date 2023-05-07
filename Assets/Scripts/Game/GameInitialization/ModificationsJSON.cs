using System;
using System.Collections.Generic;

namespace Game {
    [Serializable]
    public class ModificationsJSON {
        public List<ModificationJSONData> ModificationJsonDatas;
    }
    
    
    [Serializable]
    public class ModificationJSONData {

        string type;
        string name;
        string numberToModifyName;
        string modificationFormula;

        //pending+mission
        int turnsToComplete;
        string successChanceFormula;
        string imageAssetLink;
        string borderAssetLink;
        
    }
}