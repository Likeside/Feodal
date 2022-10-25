using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities.OdinEditor;

namespace Utilities {
    public class PanelManager: LocalSingleton<PanelManager> {

        [SerializeField] UiElementsConfigSO _elementsActiveness;
        
        [SerializeField] PanelEffectsConfigSO _panelEffectsConfigSo;
        [Header("Panels")]
        [SerializeField] GameObject _pausePanel;
        [SerializeField] GameObject _gameCompletePanel;
        [SerializeField] GameObject _settingsPanel;
        [SerializeField] GameObject _infoPanel;
        [SerializeField] GameObject _policyPanel;
        [SerializeField] GameObject _agePanel;
        [SerializeField] GameObject _topPanel;
        [SerializeField] GameObject _rewardedFailedPanel;
        [SerializeField] GameObject _shopPanel;
        [SerializeField] GameObject _rateUsPanel;
        
        [Header("TutorialPanel")]
        [SerializeField] GameObject _tutPanel;
        [SerializeField] Image _tutorialImage;
        [SerializeField] TextSetter _tutorialText;
        [SerializeField] TextSetterMesh _versionText;
        
        public PanelEffectsConfigSO PanelEffectsConfigSo => _panelEffectsConfigSo;
        public UiElementsConfigSO ElementsActiveness => _elementsActiveness;

        public GameObject ShopPanel {
            get => _shopPanel;
            set => _shopPanel = value;
        }

        void Start() {
           // SceneLoader.Instance.BsFadeIn();
            if (_policyPanel != null) {
                if (AppPolicyManager.Instance.AppPolicyAccepted) {
                    _policyPanel.SetActive(false);
                    if(AdsAndAnalyticsManager.Instance == null) Debug.Log("Instance null");
                    AdsAndAnalyticsManager.Instance.Initialize();
                }
                else {
                    _policyPanel.SetActive(true);
                }
            }
            if (_agePanel != null) {
                _agePanel.SetActive(!AppPolicyManager.Instance.UserHasProvidedAge());
            }

            if (_versionText != null) {
                _versionText.Append(LevelTracker.Version + " (C) 2022 - " + DateTime.Now.Year);
            }
            SetPanelsActiveness();

            if (_elementsActiveness.rewardedFailedPanelActive) {
                AdsAndAnalyticsManager.Instance.OnAdCanNotBeShown += ToggleRewardedFailedPanel;
            }
        }
        
        public void TogglePausePanel() { 
            if(!_elementsActiveness.pausePanelActive) return;
          AdsAndAnalyticsManager.Instance.ToggleBanner(_pausePanel.GetComponent<PanelEffect>().Hidden);
          TogglePanel(_pausePanel);
        }
        
        public void ToggleGameCompletePanel() {
            if(!_elementsActiveness.gameCompletePanelActive) return;
            AdsAndAnalyticsManager.Instance.ToggleBanner(_gameCompletePanel.GetComponent<PanelEffect>().Hidden);
          TogglePanel(_gameCompletePanel);
        }
        
        public void ToggleSettingsPanel() {
            if(!_elementsActiveness.settingsPanelActive) return;
            TogglePanel(_settingsPanel);
            if(_elementsActiveness.infoPanelActive && _infoPanel != null) TogglePanel(_infoPanel, false);
        }
        
        public void ToggleInfoPanel() {
            if(!_elementsActiveness.infoPanelActive) return;
            TogglePanel(_infoPanel);
           if(_elementsActiveness.settingsPanelActive && _settingsPanel != null) TogglePanel(_settingsPanel, false);
        }
        public void ToggleTutorialPanel() {
            if(!_elementsActiveness.tutorialPanelActive) return;
            TogglePanel(_tutPanel);
        }

        public void ToggleRewardedFailedPanel() {
            if(!_elementsActiveness.rewardedFailedPanelActive) return;
            TogglePanel(_rewardedFailedPanel);
        }
        
        public void ToggleTutorialPanel(Sprite image, string tutTextKey) {
            if(!_elementsActiveness.tutorialPanelActive) return;
            _tutorialImage.sprite = image;
            _tutorialText.SetText(tutTextKey);
            ToggleTutorialPanel();
        }

        public void ToggleShopPanel() {
            TogglePanel(_shopPanel);
        }

        public void ToggleRateUsPanel() {
            TogglePanel(_rateUsPanel);
        }
        
        public void ClosePolicyPanel() {
            _policyPanel.SetActive(false);
            AppPolicyManager.Instance.AcceptAppPolicy();
            AdsAndAnalyticsManager.Instance.LogFirstGameEnter();
            AdsAndAnalyticsManager.Instance.Initialize();
        }
        
        public void CloseAgePanel() {
            _agePanel.SetActive(false);
        }
        
        void TogglePanel(GameObject panel) {
           var panelEffect = panel.GetComponent<PanelEffect>();
           if (panelEffect.Hidden) {
               panelEffect.Show();
           }
           else {
               panelEffect.Hide();
           }
        }
        
        void TogglePanel(GameObject panel, bool on) {
            var panelEffect = panel.GetComponent<PanelEffect>();
            if (on) {
                panelEffect.ShowFromCurrentPos();
            }
            else {
                panelEffect.HideFromCurrentPos();
            }   
        }


        void SetPanelsActiveness() {
           SetPanelActiveness(_infoPanel, _elementsActiveness.infoPanelActive);
           SetPanelActiveness(_settingsPanel, _elementsActiveness.settingsPanelActive);
           SetPanelActiveness(_tutPanel, _elementsActiveness.tutorialPanelActive);
           SetPanelActiveness(_pausePanel, _elementsActiveness.pausePanelActive);
           SetPanelActiveness(_topPanel, _elementsActiveness.topPanelActive);
           SetPanelActiveness(_rewardedFailedPanel, _elementsActiveness.rewardedFailedPanelActive);
           SetPanelActiveness(_gameCompletePanel, _elementsActiveness.gameCompletePanelActive);
        }

        void SetPanelActiveness(GameObject panel, bool active) {
          if(panel != null) panel.SetActive(active);
        }

        void OnDestroy() {
            if (_elementsActiveness.rewardedFailedPanelActive) {
                AdsAndAnalyticsManager.Instance.OnAdCanNotBeShown -= ToggleRewardedFailedPanel;
            }
        }
    }
}