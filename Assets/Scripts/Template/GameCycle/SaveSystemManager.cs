using UnityEngine;
using Utilities;

namespace Template.GameCycle {
    public class SaveSystemManager: GlobalSingleton<SaveSystemManager>, ISaveSystem {
        [SerializeField] SaveSystemConfigSO _config;

        ISaveSystem _saveSystem;
        protected override void OnSingletonAwake() {
            switch (_config.saveSystemType) {
                case SaveSystemType.Binary:
                    _saveSystem = new SaveSystemBinary();
                    break;
                case SaveSystemType.Json:
                    _saveSystem = new SaveSystemJson();
                    break;
                default:
                    _saveSystem = new SaveSystemJson();
                    break;
            }
        }
        
        public void SaveGame() {
            _saveSystem.SaveGame();
        }

        public SaveData LoadGame() {
            return _saveSystem.LoadGame();
        }

        public void ClearData() {
            _saveSystem.ClearData();
        }
    }
}