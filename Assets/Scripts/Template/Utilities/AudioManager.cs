using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilities.OdinEditor;

namespace Utilities {
    public class AudioManager: LocalSingleton<AudioManager> {
        [SerializeField] Slider _volumeSlider;
        [SerializeField] List<Sprite> _soundBtnSprites;
        [SerializeField] List<Sprite> _musicBtnSprites;
        [SerializeField] Image _soundBtnImage;
        [SerializeField] Image _musicBtnImage;
        [SerializeField] AudioBasicConfigSO _basicConfigSo;
        [SerializeField] AudioSource _uiAudioSource;
        [SerializeField] UiElementsConfigSO _elementsActiveness;


        bool _musicOn;
        void Start() {
            Debug.Log("Volume: " + PlayerPrefs.GetFloat("volume") + "; Music: " + PlayerPrefsHelper.GetBool("music") );
            
            if (_elementsActiveness.volumeSliderActive) {
                _volumeSlider.onValueChanged.RemoveAllListeners();
                InitVolume();
                _volumeSlider.onValueChanged.AddListener(SetMasterVolume);
            }

            if (_elementsActiveness.soundButtonActive) {
                _soundBtnImage.sprite = PlayerPrefs.GetFloat("volume") <= 0 ? _soundBtnSprites[1] : _soundBtnSprites[0];
            }

            if (_elementsActiveness.musicButtonActive) {
                _musicOn = PlayerPrefsHelper.GetBool("music", true);
                _musicBtnImage.sprite = _musicOn ? _musicBtnSprites[0] : _musicBtnSprites[1];
                if (PlayerPrefs.GetFloat("volume") <= 0) {
                    _musicBtnImage.sprite = _musicBtnSprites[1];
                }
            }
        }
        
        void SetMasterVolume(float value) {
            Debug.Log("Setting volume: " + value);
            PlayerPrefs.SetFloat("volume", value);
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
            if (value <= 0) {
                _soundBtnImage.sprite = _soundBtnSprites[1];
                _musicBtnImage.sprite = _musicBtnSprites[1];
            }
            else {
                _soundBtnImage.sprite = _soundBtnSprites[0];
                _musicBtnImage.sprite = _musicOn ? _musicBtnSprites[0] : _musicBtnSprites[1];
            }
        }

        public void ToggleSoundButton() {
            bool on = PlayerPrefsHelper.GetBool("volume", true);
            if (on) {
                _soundBtnImage.sprite = _soundBtnSprites[1];
                PlayerPrefs.SetFloat("volume", 0f);
                _volumeSlider.value = 0f;
                AudioListener.volume = 0f;
            }
            else {
                _soundBtnImage.sprite = _soundBtnSprites[0];
                PlayerPrefs.SetFloat("volume", 1f);
                InitVolume();
            }
            PlayerPrefsHelper.SetBool("volume", !on);
        }

        public void ToggleMusicButton() {
            _musicOn = !PlayerPrefsHelper.GetBool("music", true);
            PlayerPrefsHelper.SetBool("music", _musicOn);
            _musicBtnImage.sprite = _musicOn ? _musicBtnSprites[0] : _musicBtnSprites[1];
            if (PlayerPrefs.GetFloat("volume") <= 0) {
                _musicBtnImage.sprite = _musicBtnSprites[1];
            }
        }
        
        public void PlayTapSound() {
            Debug.Log("Playing tap sound");
            _uiAudioSource.PlayOneShot(_basicConfigSo.btnClick);
        }


        void InitVolume() {
            float currentVolume = PlayerPrefs.GetFloat("volume", 1f);
            _volumeSlider.value = currentVolume;
            AudioListener.volume = currentVolume;
        }
    }
}