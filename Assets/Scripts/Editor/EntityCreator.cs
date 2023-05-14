using System;
using System.IO;
using Game;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Editor {
    public class EntityCreator<T, D>: OdinEditorWindow where T: OdinEditorWindow where D: IJson{

        public static JSONDataPathsContainer s_container;
        protected TextAsset _textAsset;
        protected D _jsonData;

        public static void OpenWindow() {
            GetWindow<T>().Show();
        }

        [Button]
        public virtual void Create() {
            LoadTextAsset();
        }

        [Button]
        public virtual void RevertLastInput() {
            _jsonData.RemoveLastEntity();
            SerializeData();
        }


        protected virtual void SerializeData() {
            var jsonText = JsonConvert.SerializeObject(_jsonData, Formatting.Indented);
            File.WriteAllText(AssetDatabase.GetAssetPath(_textAsset), jsonText);
            EditorUtility.SetDirty(_textAsset);
        }
        protected virtual void LoadTextAsset(string path) {
            if (_textAsset == null) {
                Debug.Log("Text asset not loaded, loading text asset");
                _textAsset = Resources.Load<TextAsset>(path);
            }
        }

        protected virtual void LoadTextAsset() {
            
        }


       protected float GetFloatValue(string input) {
           if (!float.TryParse(input, out float value)) {
               
               Debug.LogError("input is incorrect: " +  (input == String.Empty? "empty" : input));
                return float.NaN;
           }
           return value;
       }

       protected int GetIntValue(string input) {
           if (!int.TryParse(input, out int value)) {
                Debug.LogError("input is incorrect: " + input);
                return int.MinValue;
           }
           return value;
       }

       bool EverythingLoaded() {
           if (_textAsset == null) {
               Debug.Log("TextAsset not loaded");
               return false;
           }
           if (s_container == null) {
               Debug.Log("Paths container not loaded");
               return false;
           }
           return true;
       }
    }
}