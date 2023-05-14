using System;
using System.Linq;
using Game;
using Game.CoreGameplay.Effect;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor {
    public class ModificationsCreator: EntityCreator<ModificationsCreator, ModificationsJSON> {


        [ValueDropdown("Type")]
        ModificationType _type;
        [TextArea(1, 10)]
        public string Name = "";
        [TextArea(1, 10)]
        public string NumberToModifyName = "";  
        [TextArea(1, 10)]
        public string ModificationFormula = "";
        [TextArea(1, 10)] 
        public string TurnsToComplete = "-1";
        [TextArea(1, 10)] 
        public string SuccessChanceFormula = "";  
        [TextArea(1, 10)] 
        public string ImageAssetLink = "";  
        [TextArea(1, 10)] 
        public string BorderAssetLink = "";
        
        
        
        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.numbersPath);
            _json ??= new ModificationsJSON();
            _json.Load(s_container.modificationsPath);
        }
        
        protected override void FillData() {
            _jsonData = new ModificationJSONData() {
                type = _type.ToString(),
                name =  Name,
                numberToModifyName = NumberToModifyName,
                modificationFormula = ModificationFormula,
                turnsToComplete = GetIntValue(TurnsToComplete),
                successChanceFormula = SuccessChanceFormula,
                imageAssetLink = ImageAssetLink,
                borderAssetLink = BorderAssetLink
                
            };

        }
        
        protected override bool IsValid(IJsonData data) {
            if ( ((ModificationJSONData)data).name == String.Empty) {
                Debug.LogError("Name empty");
                return false;
            }
            if (_json.jsonDatas.Any(_ => _.name == name)) {
                Debug.LogError("Name already exists");
                return false;
            }
            if (((ModificationJSONData) data).modificationFormula == String.Empty) {
                Debug.LogError("Formula empty");
                return false;
            }
            if (!int.TryParse(TurnsToComplete, out int t)) {
                Debug.Log("TurnsToComplete incorrect: " + TurnsToComplete);
                return false;
            }
            return true;
        }
    }
}