using Template.GameCycle;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities {
    public class SceneLoader: GlobalSingleton<SceneLoader> {
        
        [SerializeField] Image _blackScreen;
        [SerializeField] AdsAndAnalyticsConfigSO _adsConfig;
        [SerializeField] Animator _animator;
        string _sceneName;
        static readonly int FadeOut = Animator.StringToHash("FadeOut");
        static readonly int FadeIn = Animator.StringToHash("FadeIn");
        public ISaveSystem SaveSystem { get; private set; }
        

        protected override void OnSingletonAwake() {
            base.OnSingletonAwake();
            switch (PanelManager.Instance.ElementsActiveness.saveSystemType) {
                case SaveSystemType.Binary:
                    SaveSystem = new SaveSystemBinary();
                    break;
                case SaveSystemType.Json:
                    SaveSystem = new SaveSystemJson();
                    break;
                default:
                    SaveSystem = new SaveSystemJson();
                    break;
            }
        }

        void Start() {
            Debug.Log("Starting scene loader");
           // _blackScreen.material.color = Color.clear;
            LevelTracker.LevelToLoad = SaveSystem.LoadGame().LevelToLoad;
            Debug.Log("Loaded: " + LevelTracker.LevelToLoad);
        }

        public void LoadLevel(int level) {

            LevelTracker.AdCounter++;
            if (level > LevelTracker.MaxLevels) {
                PanelManager.Instance.ToggleGameCompletePanel();
            }
            else {
                LevelTracker.LevelToLoad = level;
                AdsAndAnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                if (LevelTracker.AdCounter % AdsAndAnalyticsManager.Instance.InterstitialFrequency == 0 && 
                    LevelTracker.AdCounter > 0 && LevelTracker.LevelToLoad > _adsConfig.interstitialFirstLevel) {
                    AdsAndAnalyticsManager.Instance.PlayInterstitial((() => LoadScene("Game")));   
                }
                else {
                    LoadScene("Game");
                }
            }
        }

        void LoadLevelAfterSkipping() {
            LevelTracker.LevelToLoad++;
            Debug.Log("Loading: " + LevelTracker.LevelToLoad);
            AdsAndAnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
            LoadScene("Game");
        }
        
        public void LoadNextLevel()
        {
            Debug.Log("Trying to load next level");
            LevelTracker.AdCounter++;
             LevelTracker.LevelToLoad = SaveSystem.LoadGame().LevelToLoad;
            if (LevelTracker.LevelToLoad > LevelTracker.MaxLevels) {
                //PanelManager.Instance.ToggleGameCompletePanel();
                Debug.Log("1");
                LevelTracker.LevelToLoad = 1;
                AdsAndAnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                LoadScene("Game");
            }
            else {
                Debug.Log("2");
                AdsAndAnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                if (LevelTracker.AdCounter % AdsAndAnalyticsManager.Instance.InterstitialFrequency == 0 && LevelTracker.AdCounter > 0
                    && LevelTracker.LevelToLoad > _adsConfig.interstitialFirstLevel) {
                    Debug.Log("3");
                    AdsAndAnalyticsManager.Instance.PlayInterstitial((() => LoadScene("Game")));   
                }
                else {
                    Debug.Log("4");
                    LoadScene("Game");
                }
            }
        }

        public void SkipLevel() {
            AdsAndAnalyticsManager.Instance.PlayRewarded(LoadLevelAfterSkipping);
        }

        public void LoadMainMenu() {
            Debug.Log("LoadingMenu");
            AdsAndAnalyticsManager.Instance.ToggleBanner(false);
            LoadScene("Menu");
        }

        /*
        public void BsFadeIn() {
            _blackScreen.material.DOColor(Color.clear, PanelManager.Instance.ElementsActiveness.blackScreenFadeDelay).SetUpdate(true);
            Time.timeScale = 1f;
        }
        

        public void BsFadeOut() {
          _blackScreen.material.DOColor(PanelManager.Instance.ElementsActiveness.transitionColor, PanelManager.Instance.ElementsActiveness.blackScreenFadeDelay).SetUpdate(true);
        }
        */
        public void Quit() {
            SaveSystem.SaveGame();
            Application.Quit();
        }

        void OnApplicationFocus(bool hasFocus) {
            if(!hasFocus) SaveSystem.SaveGame();
        }


        void LoadScene(string sceneName) {
            SaveSystem.SaveGame();
            /*
            var tween = _blackScreen.material.DOColor(PanelManager.Instance.ElementsActiveness.transitionColor,
                PanelManager.Instance.ElementsActiveness.blackScreenFadeDelay).SetUpdate(true);
            tween.onComplete = () => SceneManager.LoadScene(sceneName);
            */
            _animator.SetTrigger(FadeOut);
            _sceneName = sceneName;
        }

        public void OnFadeComplete() {
            SceneManager.LoadScene(_sceneName);
            _animator.SetTrigger(FadeIn);
        }
    }
    
}