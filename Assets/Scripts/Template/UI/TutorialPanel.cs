using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace Template.UI {
    public class TutorialPanel: MonoBehaviour {
        [SerializeField] Image _tutorialImage;
        [SerializeField] TextSetter _tutorialText;


        public void Set(Sprite image, string tutTextKey) {
            _tutorialImage.sprite = image;
            _tutorialText.SetText(tutTextKey);
        }
    }
}