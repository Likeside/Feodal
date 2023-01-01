using Template.AdsAndAnalytics;
using Template.GameCycle;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Utilities {
    public class SceneLoader: GlobalSingleton<SceneLoader> {
        
        [SerializeField] Animator _animator;
        [SerializeField] float _fadeSpeed;
        string _sceneName;
        static readonly int FadeOut = Animator.StringToHash("FadeOut");
        static readonly int FadeIn = Animator.StringToHash("FadeIn");
        

        protected override void OnSingletonAwake() {
            base.OnSingletonAwake();
            _animator.speed = _fadeSpeed;
        }

        void Start() {
            Debug.Log("Starting scene loader");
           // _blackScreen.material.color = Color.clear;
            LevelTracker.LevelToLoad = SaveSystemManager.Instance.LoadGame().LevelToLoad;
            Debug.Log("Loaded: " + LevelTracker.LevelToLoad);
        }

        public void LoadLevel(int level) {

            if (level > LevelTracker.MaxLevels) {
                PanelManager.Instance.ToggleGameCompletePanel();
            }
            else {
                LevelTracker.LevelToLoad = level;
                AnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                AdsManager.Instance.PlayInterstitial((() => LoadScene("Game")), LevelTracker.LevelToLoad);
            }
        }

        void LoadLevelAfterSkipping() {
            LevelTracker.LevelToLoad++;
            Debug.Log("Loading: " + LevelTracker.LevelToLoad);
            AnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
            LoadScene("Game");
        }
        
        public void LoadNextLevel()
        {
            Debug.Log("Trying to load next level");
             LevelTracker.LevelToLoad = SaveSystemManager.Instance.LoadGame().LevelToLoad;
            if (LevelTracker.LevelToLoad > LevelTracker.MaxLevels) {
                //PanelManager.Instance.ToggleGameCompletePanel();
                LevelTracker.LevelToLoad = 1;
                AnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                LoadScene("Game");
            }
            else {
                AnalyticsManager.Instance.LogLevelStart(LevelTracker.LevelToLoad);
                AdsManager.Instance.PlayInterstitial((() => LoadScene("Game")), LevelTracker.LevelToLoad);
            }
        }

        public void SkipLevel() {
            AdsManager.Instance.PlayRewarded(LoadLevelAfterSkipping);
        }

        public void LoadMainMenu() {
            Debug.Log("LoadingMenu");
            AdsManager.Instance.ToggleBanner(false);
            LoadScene("Menu");
        }
        
        public void Quit() {
            SaveSystemManager.Instance.SaveGame();
            Application.Quit();
        }

        void OnApplicationFocus(bool hasFocus) {
            if(!hasFocus) SaveSystemManager.Instance.SaveGame();
        }
        
        void LoadScene(string sceneName) {
            SaveSystemManager.Instance.SaveGame();
            _animator.SetTrigger(FadeOut);
            _sceneName = sceneName;
        }

        public void OnFadeComplete() {
            SceneManager.LoadScene(_sceneName);
            _animator.SetTrigger(FadeIn);
        }
    }
    
}