using System.IO;
using System.Linq;
using Game;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Formatting = Unity.Plastic.Newtonsoft.Json.Formatting;
using JsonConvert = Unity.Plastic.Newtonsoft.Json.JsonConvert;

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
        


        public override void Create() {
            base.Create();
            NumberJSONData numberJsonData = new NumberJSONData();
            numberJsonData.name = Name;
            numberJsonData.initValue = GetFloatValue(InitValue);
            numberJsonData.minValue = GetFloatValue(MinValue);
            numberJsonData.maxValue = GetFloatValue(MaxValue);
            numberJsonData.formula = Formula;
            if(!IsValid(numberJsonData)) return;
            _jsonData.AddEntity(numberJsonData);
            SerializeData();
        }
        

        protected override void LoadTextAsset() {
            LoadTextAsset(s_container.numbersPath);
            _jsonData ??= new NumbersJSON();
            _jsonData.Load(s_container.numbersPath);
        }
        
        bool IsValid(NumberJSONData data) {
            if (data.name == string.Empty) {
                Debug.LogError("Name empty");
                return false;
            }
            if (_jsonData.jsonDatas.Any(_ => _.name == name)) {
                Debug.LogError("Name already exists");
                return false;
            }

            if (float.IsNaN(data.initValue) || float.IsNaN(data.minValue) || float.IsNaN(data.maxValue)) {
                Debug.LogError("Values incorrect");
                return false;
            }

            return true;
        }
        
    }
}