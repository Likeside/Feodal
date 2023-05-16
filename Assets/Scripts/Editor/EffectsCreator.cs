using System.Collections.Generic;
using System.Linq;
using Game;
using Game.CoreGameplay.Effect;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor {
    public class EffectsCreator: EntityCreator<EffectsCreator, EffectsJSON> {
        
        public static List<string> s_numbersNames;

        public EffectType Type;
        [ValueDropdown(nameof(s_numbersNames))]
        public string CountNumber = s_numbersNames[0]; 
        [TextArea(1, 50)]
        public string ModificationsNames = "";
        [TextArea(1, 10)] 
        public string InitTurns = "-1";
        [TextArea(1, 50)] 
        public string TurnModificationNumberName = s_numbersNames[0];
        [TextArea(1, 50)] 
        public string Description = "";    
        [TextArea(1, 50)] 
        public string Subdescription = "";  
        [TextArea(1, 50)] 
        public string ImageAssetLink = "";  
        [TextArea(1, 50)] 
        public string BorderAssetLink = "";
 
        
        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.effectsPath);
            _json ??= new EffectsJSON();
            _json.Load(s_container.effectsPath);
        }

        protected override void FillData() {
            _jsonData = new EffectJSONData() {
                type = Type.ToString(),
                countNumberName = CountNumber,
                modificationsName = ModificationsNames.Split(",").ToList(),
                initTurns = GetIntValue(InitTurns),
                turnModificationNumberName = TurnModificationNumberName,
                description = Description,
                subdescription = Subdescription,
                imageAssetLink = ImageAssetLink,
                borderAssetLink = BorderAssetLink
            };
        }

        protected override bool IsValid(IJsonData data) {
            if (((EffectJSONData) data).modificationsName.Count < 1) {
                Debug.LogError("No modifications added");
                return false;
            }
            return true;
        }
    }
}