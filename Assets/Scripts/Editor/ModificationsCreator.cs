using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.CoreGameplay.Effect;
using Sirenix.OdinInspector;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Editor {
    public class ModificationsCreator: EntityCreator<ModificationsCreator, ModificationsJSON> {
        
       public static List<string> s_numbersNames;

        public ModificationType type;
        [TextArea(1, 10)]
        public string Name = "";
        [ValueDropdown(nameof(s_numbersNames))]
        public string NumberToModifyName = s_numbersNames[0];  
        [TextArea(1, 50)]
        public string ModificationFormula = "";
        [TextArea(1, 10)] 
        public string TurnsToComplete = "-1";
        [TextArea(1, 50)] 
        public string SuccessChanceFormula = "";  
        [TextArea(1, 50)] 
        public string ImageAssetLink = "";  
        [TextArea(1, 50)] 
        public string BorderAssetLink = "";

        
        
        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.modificationsPath);
            _json ??= new ModificationsJSON();
            _json.Load(s_container.modificationsPath);
        }
        
        protected override void FillData() {
            _jsonData = new ModificationJSONData {
                type = type.ToString(),
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
            if (((ModificationJSONData)data).name == String.Empty) {
                Debug.LogError("Name empty");
                return false;
            }
            if (_json.jsonDatas.Any(_ => _.name == ((ModificationJSONData)data).name)) {
                Debug.LogError("Name already exists");
                return false;
            }
            if (((ModificationJSONData) data).modificationFormula == String.Empty) {
                Debug.LogError("Formula empty");
                return false;
            }
            if (!int.TryParse(TurnsToComplete, out int t)) {
                Debug.LogError("TurnsToComplete incorrect: " + TurnsToComplete);
                return false;
            }
            if (s_numbersNames.All(_ => _ != ((ModificationJSONData) data).numberToModifyName)) {
                Debug.LogError("No number with such name");
                return false;
            }
            if (type == ModificationType.Mission &&
                ((ModificationJSONData) data).successChanceFormula == String.Empty) {
                Debug.LogError("Type of mod is mission, but no success chance formula entered");
                return false;
            }
            return true;
        }
    }
}