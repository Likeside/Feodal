using UnityEngine;

namespace Template.AdsAndAnalytics {
    [CreateAssetMenu(fileName = "AnalyticsConfigSO", menuName = "Configs/AnalytivsConfigSO", order = 5)]

    public class AnalyticsConfigSO: ScriptableObject {
        public bool isActive;
    }
}