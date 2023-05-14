using Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Editor {
    public class OdinWindowManager: MonoBehaviour {

        [SerializeField] JSONDataPathsContainer _container;
        
        [Button]
        public void CreateNumber() {
            NumbersCreator.s_container = _container;
            NumbersCreator.OpenWindow();
        }


        [Button]
        public void CreateModification() {
            
        }
    }
}