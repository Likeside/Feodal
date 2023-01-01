using UnityEngine;

namespace Template.AdsAndAnalytics {
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