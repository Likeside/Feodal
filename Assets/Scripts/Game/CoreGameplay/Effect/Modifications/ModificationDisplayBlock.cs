using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Game.CoreGameplay.Effect {
    public class ModificationDisplayBlock: MonoBehaviour {
        [SerializeField] Image _image;
        [SerializeField] Image _imageBorder;
        [SerializeField] TextMeshProUGUI _text;

        //mock
        public void SetColor(Color color) {
            _image.color = color;
        }
    }
}