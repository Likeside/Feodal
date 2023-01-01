using System;
using Template.AdsAndAnalytics;
using UnityEngine;
using Utilities;

namespace Template.UI {
    public class PanelAndAdsConnector: MonoBehaviour {
        void Start() {
            PanelManager.Instance.OnShouldInit += InitManagers;
            PanelManager.Instance.OnBannerTime += AdsManager.Instance.ToggleBanner;
            if (PanelManager.Instance.ElementsActiveness.rewardedFailedPanelActive) {
                AdsManager.Instance.OnAdCanNotBeShown += PanelManager.Instance.ToggleRewardedFailedPanel;
            }
            PanelManager.Instance.Init();
        }

        void InitManagers(bool firstEnter) {
            if (firstEnter) {
                AnalyticsManager.Instance.LogFirstGameEnter();
            }
            AdsManager.Instance.Initialize();

        }
        
        void OnDestroy() {
            PanelManager.Instance.OnShouldInit -= InitManagers;
            PanelManager.Instance.OnBannerTime -= AdsManager.Instance.ToggleBanner;
            if (PanelManager.Instance.ElementsActiveness.rewardedFailedPanelActive) {
                AdsManager.Instance.OnAdCanNotBeShown -= PanelManager.Instance.ToggleRewardedFailedPanel;

            }
        }
    }
}