using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace Editor {
    public class EntityCreator: OdinEditorWindow {
        
        public static void OpenWindow() {
            GetWindow<EntityCreator>().Show();
        }

        [Button]
        public void Test() {
            
        }
    }
}