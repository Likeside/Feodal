using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using Utilities;

namespace Template.AdsAndAnalytics {
    public class AnalyticsManager: GlobalSingleton<AnalyticsManager> {

        [SerializeField] AnalyticsConfigSO _config;
        
        const string k_isFirstEnter = "isFirstEnter";

        public void LogFirstGameEnter() {
            if(!_config.isActive) return;
            bool isFirstEnter = PlayerPrefsHelper.GetBool(k_isFirstEnter, true);
            if (isFirstEnter) {
                PlayerPrefsHelper.SetBool(k_isFirstEnter, false);
                Analytics.CustomEvent("FIRST_LAUNCH");
            }
        }
        public void LogLevelStart(int level) {
            if(!_config.isActive) return;
            Analytics.CustomEvent("LEVEL_START", new Dictionary<string, object>() { {"N", level}});
        }
        public void LogPlayerDeath(int level) {
            if(!_config.isActive) return;
            Analytics.CustomEvent("PLAYER_DEATH", new Dictionary<string, object>() { {"N", level}});
        }
        public void LogLevelComplete(int level) {
            if(!_config.isActive) return;
            Analytics.CustomEvent("LEVEL_COMPLETE", new Dictionary<string, object>() { {"N", level}});
        }
        public void LogLevelSkip(int level) {
            if(!_config.isActive) return;
            Analytics.CustomEvent("LEVEL_SKIP", new Dictionary<string, object>() { {"N", level}});
        }
    }
}