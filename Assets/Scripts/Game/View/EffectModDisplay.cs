using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.View {
    public class EffectModDisplay: MonoBehaviour {

        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _text;

        [SerializeField] Image _missionSuccessImage;
        [SerializeField] TextMeshProUGUI _missionSuccessText;


        public void Set(Sprite sprite, string text) {
            _image.sprite = sprite;
            _text.text = text;
        }

        public void SetMissionPercent(string text) {
            _missionSuccessText.text = text;
        }
    }
}