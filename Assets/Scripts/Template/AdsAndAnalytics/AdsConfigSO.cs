using UnityEngine;

namespace Template.AdsAndAnalytics {
    [CreateAssetMenu(fileName = "AdsConfigSO", menuName = "Configs/AdsConfigSO", order = 5)]

    public class AdsConfigSO: ScriptableObject {
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
    }
}