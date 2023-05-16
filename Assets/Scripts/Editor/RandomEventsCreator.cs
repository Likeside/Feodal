using System.Collections.Generic;
using Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor {
    public class RandomEventsCreator: EntityCreator<RandomEventsCreator, EventsJSON> {
        
        public static List<string> s_numbersNames;
        
        [ValueDropdown(nameof(s_numbersNames))]
        public string EffectCountNumberName = s_numbersNames[0];     
        
        [ValueDropdown(nameof(s_numbersNames))]
        public string ProbabilityNumberName = s_numbersNames[0]; 
        
        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.randomEventsPath);
            _json ??= new EventsJSON();
            _json.Load(s_container.randomEventsPath);
        }
        protected override void FillData() {
            _jsonData = new EventsJSONData() {
                effectCountNumberName = EffectCountNumberName,
                probabilityNumberName = ProbabilityNumberName
            };
        }
        protected override bool IsValid(IJsonData data) {
            if (EffectCountNumberName == ProbabilityNumberName) {
                Debug.LogError("EffectCountNumber and ProbabilityNumber can't be the same");
                return false;
            }
            return true;
        } 
    }
}