using System;
using Template.AdsAndAnalytics;
using Template.UI;
using UnityEngine;
using Utilities;

public class GameController : LocalSingleton<GameController> {
        
        [SerializeField] ButtonManager _buttonManager;


        void Start() { 
                //TODO: Отписываться в определенный момент игры
                if (_buttonManager.TipButtonActive) _buttonManager.OnTipButtonPressed += TipButtonPressed;
                if (_buttonManager.PauseButtonActive) _buttonManager.OnTipButtonPressed += PauseButtonPressed;
                if (PanelManager.Instance.ElementsActiveness.rateUsPanelActive) ExternalLinksManager.Instance.OnRateUsLinkOpened += RateUsLinkOpened;
        }

        void RateUsLinkOpened() {
                Debug.Log("Rate us link opened");
        }

        void PauseButtonPressed() {
                Debug.Log("Pause button pressed");
        }

        void TipButtonPressed() {
                Debug.Log("Tip button pressed");
        }
        
}
