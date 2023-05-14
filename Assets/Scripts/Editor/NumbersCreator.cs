using System.IO;
using Game;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Formatting = Unity.Plastic.Newtonsoft.Json.Formatting;
using JsonConvert = Unity.Plastic.Newtonsoft.Json.JsonConvert;

namespace Editor {
    public class NumbersCreator: EntityCreator<NumbersCreator> {

        NumbersJSON _numbersJson;
        
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
            _numbersJson.jsonDatas.Add(numberJsonData);
            var jsonText = JsonConvert.SerializeObject(_numbersJson, Formatting.Indented);
            File.WriteAllText(AssetDatabase.GetAssetPath(_textAsset), jsonText);
            EditorUtility.SetDirty(_textAsset);
        }

        protected override void LoadTextAsset() {
            LoadTextAsset(s_container.numbersPath);
            if (_numbersJson == null) {
                _numbersJson = new NumbersJSON();
            }
            _numbersJson.Load(s_container.numbersPath);
        }
        
    }
}