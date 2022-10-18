using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utilities.OdinEditor;

namespace Template.UI {
    
    [ExecuteInEditMode]
    public class LayoutGroupsSettings: MonoBehaviour {
        [SerializeField] bool _isLandscape;
        
        
        [Button]
        public void CopyCurrentGroupData() {
            var layoutGroups = FindObjectsOfType<HorizontalOrVerticalLayoutGroup>(true);
            foreach (var group in layoutGroups) {
                var presentDatas = group.gameObject.GetComponents<LayoutGroupData>();
                if (presentDatas.Length > 1) {
                    Debug.Log("This group already has two or more datas");
                    continue;
                }
                var data = group.gameObject.AddComponent<LayoutGroupData>();
               data.CopyData(_isLandscape);
            }
        }

        [Button]
        public void SetData() {
            var layoutGroups = FindObjectsOfType<HorizontalOrVerticalLayoutGroup>(true);
            foreach (var group in layoutGroups) {
                var data = group.gameObject.GetComponents<LayoutGroupData>().FirstOrDefault(d => d.IsLandscape == _isLandscape);
                if (data != null) {
                    data.SetData();
                }
                else {
                    Debug.Log("Some of the datas are absent, aborting");
                    return;
                }
            }

            var rectSettings = FindObjectsOfType<RectSettings>(true);

            foreach (var rectSetting in rectSettings) {
                if (rectSetting.IsLandscape == _isLandscape) {
                    rectSetting.gameObject.SetActive(true);
                }
                else {
                    rectSetting.gameObject.SetActive(false);
                }
            }
            
        }

        [Button]
        public void DeleteLayoutGroupDatas() {
            var datas = FindObjectsOfType<LayoutGroupData>(true);
            foreach (var data in datas) {
                DestroyImmediate(data);
            }
        }
    }
}