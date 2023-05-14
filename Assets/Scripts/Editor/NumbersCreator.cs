using System;
using System.Linq;
using Game;
using UnityEngine;


namespace Editor {
    public class NumbersCreator: EntityCreator<NumbersCreator, NumbersJSON> {

        
        [TextArea(1, 10)]
        public string Name = "";
        [TextArea(1, 10)]
        public string InitValue = "";
        [TextArea(1, 10)]
        public string MinValue = ""; 
        [TextArea(1, 10)]
        public string MaxValue = ""; 
        [TextArea(1, 10)]
        public string Formula = "";
        
        
        //overrides
        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.numbersPath);
            _json ??= new NumbersJSON();
            _json.Load(s_container.numbersPath);
        }

        protected override void FillData() {
            _jsonData = new NumberJSONData {
                name = Name,
                initValue = GetFloatValue(InitValue),
                minValue = GetFloatValue(MinValue),
                maxValue = GetFloatValue(MaxValue),
                formula = Formula
            };
        }
        protected override bool IsValid(IJsonData data) {
            if ( ((NumberJSONData)data).name == String.Empty) {
                Debug.LogError("Name empty");
                return false;
            }
            if (_json.jsonDatas.Any(_ => _.name == ((NumberJSONData)data).name)) {
                Debug.LogError("Name already exists");
                return false;
            }

            if (float.IsNaN(((NumberJSONData)data).initValue) || float.IsNaN(((NumberJSONData)data).minValue) || float.IsNaN(((NumberJSONData)data).maxValue)) {
                Debug.LogError("Values incorrect");
                return false;
            }

            return true;
        }
        
    }
}