using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Analytics;

namespace Utilities {
    public class AdsAndAnalyticsManagerX: GlobalSingleton<AdsAndAnalyticsManagerX>, IUnityAdsListener {

        [SerializeField] AdsAndAnalyticsConfigSOX _config;

        public event Action OnRewardedShown;
        public event Action OnInterstitialShown;

        public event Action OnAdCanNotBeShown;

        public event Action OnRateUsLinkOpened;
        public bool RewardedReady => Advertisement.IsReady(_rewarded);

        public int InterstitialFrequency => _config.interstitialFrequency;
        
        
        const string k_isFirstEnter = "isFirstEnter";
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
        
        //analytics
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
        
        //ads
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

        public void OpenTermsLink() {
            Debug.Log("Opening terms link");
            StartCoroutine(OpenLinkCoroutine(_config.termsLink));
        }

        public void OpenPolicyLink() {
            Debug.Log("Opening policy link");
            Application.OpenURL(_config.policyLink);
        }

        public void OpenRateUsLink() {
            Debug.Log("Opening rate us link");
            StartCoroutine(OpenLinkCoroutine(_config.rateUsLink));
            OnRateUsLinkOpened?.Invoke();
        }

        IEnumerator OpenLinkCoroutine(string url) {
            yield return new WaitForSeconds(0.1f);
            Application.OpenURL(url);
        }
    }
}