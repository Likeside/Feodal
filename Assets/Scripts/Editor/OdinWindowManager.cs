using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Editor {
    public class OdinWindowManager: MonoBehaviour {
        

        [Button]
        public void CreateNumber() {
            EntityCreator.OpenWindow();
        }


        [Button]
        public void CreateModification() {
            
        }
    }
}