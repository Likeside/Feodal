using System.Collections.Generic;
using System.Linq;
using Game;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor {
    public class OdinWindowManager: MonoBehaviour {

        [SerializeField] JSONDataPathsContainer _container;
        
         TextAsset _numbersTextAsset;
         List<NumberJSONData> _numberJsonDatas;
        
        
        [Button]
        public void CreateNumber() {
            NumbersCreator.s_container = _container;
            NumbersCreator.OpenWindow();
        }


        [Button]
        public void CreateModification() {
            LoadNumbers();
            ModificationsCreator.s_numbersNames = _numberJsonDatas.Select(_ => _.name).ToList();
            ModificationsCreator.s_container = _container;
            ModificationsCreator.OpenWindow();
        }

        [Button]
        public void CreateEffect() {
            
        }



        void LoadNumbers() {
            _numbersTextAsset = Resources.Load<TextAsset>(_container.numbersPath);
            _numberJsonDatas = JsonConvert.DeserializeObject<NumbersJSON>(_numbersTextAsset.text).jsonDatas;
        }
    }
}