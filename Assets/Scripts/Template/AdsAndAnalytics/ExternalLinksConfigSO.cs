using UnityEngine;

namespace Template.AdsAndAnalytics {
    
    [CreateAssetMenu(fileName = "ExternalLinksConfigSO", menuName = "Configs/ExternalLinksConfigSO", order = 5)]

    public class ExternalLinksConfigSO: ScriptableObject {
        public string termsLink;
        public string policyLink;
        public string rateUsLink;
    }
}