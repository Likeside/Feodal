using UnityEngine;

namespace Template.GameCycle {
    [CreateAssetMenu(fileName = "SaveSystemConfigSO", menuName = "Configs/SaveSystemConfigSO", order = 5)]
    public class SaveSystemConfigSO: ScriptableObject {
        public SaveSystemType saveSystemType;
    }
}