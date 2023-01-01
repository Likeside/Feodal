using System;
using System.Collections.Generic;
using Template.AdsAndAnalytics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utilities;
using Utilities.OdinEditor;

namespace Template.UI {
    public class ButtonManager: MonoBehaviour {
        
        [SerializeField] Button _startBtn;
        [SerializeField] Button _quitButton;
        [SerializeField] Button _settingsButton;
        [SerializeField] Button _infoButton;
        [SerializeField] Button _backButton;
        [SerializeField] Button _tipButton;
        [SerializeField] Button _skipLevelButton;
        [SerializeField] Button _pauseButton;
        [SerializeField] Button _infoPanelCloseButton;
        [SerializeField] Button _settingsPanelCloseButton;
        [SerializeField] Button _enButton;
        [SerializeField] Button _ruButton;
        [SerializeField] Button _musicButton;
        [SerializeField] Button _soundButton;
        [SerializeField] Button _closeShopButton;
        [SerializeField] Button _shopButton;
        [SerializeField] Button _rateUsYes;
        [SerializeField] Button _rateUsNo;
        [SerializeField] Button[] _rewardedFailedPanelCloseButtons;
        [SerializeField] Button[] _policyButtons;
        [SerializeField] Button[] _termsButtons;

        [Header("OnlySound")]
        [SerializeField] Button[] _soundSubs;
        public event Action OnTipButtonPressed;
        public event Action OnPauseButtonPressed;
        public bool TipButtonActive => PanelManager.Instance.ElementsActiveness.tipButtonActive;
        public bool PauseButtonActive => PanelManager.Instance.ElementsActiveness.pauseButtonActive;
        void Start() {
            Debug.Log("Button manager start");
            SetButton(_settingsButton, PanelManager.Instance.ElementsActiveness.settingsPanelActive, PanelManager.Instance.ToggleSettingsPanel);
            SetButton(_infoButton, PanelManager.Instance.ElementsActiveness.infoPanelActive, PanelManager.Instance.ToggleInfoPanel);
            SetButton(_backButton, PanelManager.Instance.ElementsActiveness.backButtonActive, SceneLoader.Instance.LoadMainMenu);
            SetButton(_tipButton, PanelManager.Instance.ElementsActiveness.tipButtonActive, (() => OnTipButtonPressed?.Invoke()));
            SetButton(_skipLevelButton, PanelManager.Instance.ElementsActiveness.skipLevelButtonActive, SceneLoader.Instance.SkipLevel);
            SetButton(_startBtn, true, SceneLoader.Instance.LoadNextLevel);
            SetButton(_quitButton, PanelManager.Instance.ElementsActiveness.quitButtonActive, SceneLoader.Instance.Quit);
            SetButton(_pauseButton, PanelManager.Instance.ElementsActiveness.pauseButtonActive, (() => OnPauseButtonPressed?.Invoke()) );
            SetButton(_infoPanelCloseButton, PanelManager.Instance.ElementsActiveness.infoPanelActive, PanelManager.Instance.ToggleInfoPanel);
            SetButton(_settingsPanelCloseButton, PanelManager.Instance.ElementsActiveness.settingsPanelActive, PanelManager.Instance.ToggleSettingsPanel);
            SetButton(_enButton, PanelManager.Instance.ElementsActiveness.enButtonActive, (() => TextLoader.Instance.SwitchLocalization(Localization.EN)));
            SetButton(_ruButton, PanelManager.Instance.ElementsActiveness.ruButtonActive, (() => TextLoader.Instance.SwitchLocalization(Localization.RU)));
            SetButton(_musicButton, PanelManager.Instance.ElementsActiveness.musicButtonActive, AudioManager.Instance.ToggleMusicButton);
            SetButton(_soundButton, PanelManager.Instance.ElementsActiveness.soundButtonActive, AudioManager.Instance.ToggleSoundButton);
            SetButton(_closeShopButton, PanelManager.Instance.ElementsActiveness.shopPanelActive, PanelManager.Instance.ToggleShopPanel);
            SetButton(_shopButton, PanelManager.Instance.ElementsActiveness.shopPanelActive, PanelManager.Instance.ToggleShopPanel);
            SetButton(_rateUsNo, PanelManager.Instance.ElementsActiveness.rateUsPanelActive, PanelManager.Instance.ToggleRateUsPanel);
            SetButton(_rateUsYes, PanelManager.Instance.ElementsActiveness.rateUsPanelActive, ExternalLinksManager.Instance.OpenRateUsLink);

            foreach (var button in _rewardedFailedPanelCloseButtons) {
                SetButton(button, PanelManager.Instance.ElementsActiveness.rewardedFailedPanelActive, PanelManager.Instance.ToggleRewardedFailedPanel);
            }
            foreach (var button in _policyButtons) {
                SetButton(button, true, ExternalLinksManager.Instance.OpenPolicyLink);
            }
            foreach (var button in _termsButtons) {
                SetButton(button, true, ExternalLinksManager.Instance.OpenTermsLink);
            }

            foreach (var button in _soundSubs) {
                SubSound(button);
            }
        }
        void SetButton(Button button, bool active, UnityAction listener) {
            if (button != null) {
                button.gameObject.SetActive(active);
                if (active) {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(listener);
                    button.onClick.AddListener(AudioManager.Instance.PlayTapSound);
                }
            }
        }

        void SubSound(Button button) {
            if (button != null) {
                button.onClick.RemoveListener(AudioManager.Instance.PlayTapSound);
                button.onClick.AddListener(AudioManager.Instance.PlayTapSound);
            }
        }
        void TipButtonPressed() {
            OnTipButtonPressed?.Invoke();
        }
    }
}