using Template.GameCycle;
using UnityEngine;

namespace Utilities.OdinEditor {
    
    [CreateAssetMenu(fileName = "UIElementsConfigSO", menuName = "Configs/UIElementsConfigSO", order = 5)]
    public class UiElementsConfigSO: ScriptableObject {
        [Header("Activeness")]
        public bool settingsPanelActive = true;
        public bool infoPanelActive = true;
        public bool tutorialPanelActive;
        public bool pausePanelActive = true;
        public bool rewardedFailedPanelActive = true;
        public bool gameCompletePanelActive = true;
        public bool adminInterfaceActive;
        public bool topPanelActive = true;
        public bool quitButtonActive = true;
        public bool tipButtonActive;
        public bool pauseButtonActive = true;
        public bool backButtonActive = true;
        public bool skipLevelButtonActive = true;
        public bool ruButtonActive = true;
        public bool enButtonActive = true;
        public bool volumeSliderActive = true;
        public bool musicButtonActive = true;
        public bool soundButtonActive = true;
        public bool shopPanelActive;
        public bool rateUsPanelActive = true;
        [Header("Parameters")]
        public float blackScreenFadeDelay;
        public Color transitionColor;

    }
}