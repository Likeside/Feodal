using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using Utilities;

namespace Template.AdsAndAnalytics {
    public class AdsManager: GlobalSingleton<AdsManager>, IUnityAdsListener {
        [SerializeField] AdsConfigSO _config;
        
        public event Action OnRewardedShown;
        public event Action OnInterstitialShown;
        public event Action OnAdCanNotBeShown;
        public bool RewardedReady => Advertisement.IsReady(_rewarded);
        public int InterstitialFrequency => _config.interstitialFrequency;
        
        string _rewarded;
        string _interstitial;
        string _banner;
        
        public void Initialize() {
            if(!_config.isActive) return;
#if UNITY_ANDROID
            Advertisement.Initialize(_config.androidID);
            SetAdsPlatform(_config.androidRewarded, _config.androidInterstitial, _config.androidBanner);
#endif

#if UNITY_IOS
            Advertisement.Initialize(_iosID);
            SetAdsPlatform(_config.iosRewarded, _config.iosInterstitial, _config.iosBanner);
#endif
            Advertisement.AddListener(this);
        }
        
                public void PlayInterstitial(Action completionCallBack) {
            if (!_config.isActive) {
                Debug.Log("Ad config is disabled, dummy interstitial action called");
                completionCallBack?.Invoke();
                return;
            }
            OnInterstitialShown = completionCallBack;
            if (Advertisement.IsReady(_interstitial)) {
                Advertisement.Show(_interstitial);
            }
            else {
                OnInterstitialShown?.Invoke();
            }
        }

        public void PlayRewarded(Action completionCallBack) {
            if (!_config.isActive) {
                Debug.Log("Ad config is disabled, dummy rewarded action called");
                completionCallBack?.Invoke();
              // OnAdCanNotBeShown?.Invoke();
                return;
            }
            if (Advertisement.IsReady(_rewarded)) {
                OnRewardedShown = completionCallBack;
                Advertisement.Show(_rewarded);
            }
            else {
               OnAdCanNotBeShown?.Invoke(); 
            }
        }

        public void ToggleBanner(bool on) {
            if(!_config.isActive) return;
            if (!on) {
                Advertisement.Banner.Hide();
                return;
            }
            if (Advertisement.IsReady(_banner)) {
                Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
                Advertisement.Banner.Show(_banner);
            }
            else {
                StartCoroutine(TryShowBannerAgain());
            }
        }

        IEnumerator TryShowBannerAgain() {
            if (!_config.isActive) yield break;
            yield return new WaitForSeconds(0.5f);
            if (Advertisement.IsReady(_banner)) {
                Advertisement.Banner.Show(_banner);
            }
        }

        void SetAdsPlatform(string rewarded, string interstitial, string banner) {
            if(!_config.isActive) return;
            _rewarded = rewarded;
            _interstitial = interstitial;
            _banner = banner;
        }

        public void OnUnityAdsReady(string placementId) {
            Debug.Log("Ad is ready: " + placementId);
        }

        public void OnUnityAdsDidError(string message) {
            Debug.Log("Something is wrong with the ad: " + message);
        }

        public void OnUnityAdsDidStart(string placementId) {
            Debug.Log("Ad started: " + placementId);
        }

        public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) {
            if(!_config.isActive) return;
            Debug.Log("Ad did finish");
            if (placementId == _rewarded && showResult == ShowResult.Finished) { 
                Debug.Log("Rewarded shown");
                OnRewardedShown?.Invoke();
                OnRewardedShown = null;
            }

            if (placementId == _interstitial) {
                Debug.Log("Interstitial shown");
                OnInterstitialShown?.Invoke();
                OnInterstitialShown = null;
            }
        }
    }
}