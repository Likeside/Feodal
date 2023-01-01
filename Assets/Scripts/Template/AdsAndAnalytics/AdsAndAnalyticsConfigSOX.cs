using UnityEngine;

namespace Utilities {
    [CreateAssetMenu(fileName = "AndsAndAnalyticsConfigSO", menuName = "Configs/AdsAndAnalyticsConfigSO", order = 5)]
    public class AdsAndAnalyticsConfigSOX: ScriptableObject {

        public bool isActive;
        
        [Header("Android")]
        public string androidID;
        public string androidRewarded;
        public string androidInterstitial;
        public string androidBanner;
        
        [Header("iOS")]
        public string iosID;
        public string iosRewarded;
        public string iosInterstitial;
        public string iosBanner;

        [Header("Settings")]
        public int interstitialFrequency;
        public int interstitialFirstLevel;

        [Header("Links")] 
        public string termsLink;
        public string policyLink;
        public string rateUsLink;


    }
}