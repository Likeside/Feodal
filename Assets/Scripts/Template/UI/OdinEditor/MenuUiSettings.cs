using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Utilities.OdinEditor {
    
    [ExecuteInEditMode]
    public class MenuUiSettings: MonoBehaviour {


        [SerializeField] List<RectTransformSingleSettings> _singleRects;
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 30)]
        [SerializeField] List<RectTransformGroupSettings> _groupRects;

        [HorizontalGroup("single")]
        public string singleRectName;
        [HorizontalGroup("single")]
        [Button]
        public void AddSingleRect() {
            _singleRects.Add(new RectTransformSingleSettings(singleRectName));
            singleRectName = String.Empty;
        }

        [HorizontalGroup("group")]
        public string rectGroupName;
        [HorizontalGroup("group")]
        [Button]
        public void AddGroupRects() {
            _groupRects.Add(new RectTransformGroupSettings(rectGroupName));
            rectGroupName = String.Empty;
        }

        void Update() {
            foreach (var setting in _singleRects) {
                setting.SetSettings();
            } 
            foreach (var setting in _groupRects) {
                setting.SetSettings();
            }
        }



        [Serializable]
        public abstract class RectTransformSettings {
            
            string _header;
            [Title("$_header")] 

            [Vector2Slider(100, 2566)]
            public Vector2 _rectSize;
            public Color _color;
            
            [HorizontalGroup("Fields", 100)]
            [PreviewField]
            [HideLabel]
            public Sprite _texture;

            protected RectTransformSettings(string header) {
                _header = header;
                _color = Color.white;
            }
            
            public abstract void SetSettings();
            
            [VerticalGroup("Fields/Group")]
            [Button]
            public abstract void GetImageComponent();
        }

        
        [Serializable]
        public class RectTransformGroupSettings: RectTransformSettings {
            
            public List<RectTransform> rects;
            List<Image> images;
            public RectTransformGroupSettings(string header) : base(header) {
            }

            public override void SetSettings() {
                foreach (var rect in rects) {
                    rect.sizeDelta = _rectSize;
                }

                foreach (var image in images) {
                    image.sprite = _texture;
                    image.color = _color;
                }
            }
            
            public override void GetImageComponent() {
                if (!rects.Any()) {
                    Debug.Log("No rects added to the list");
                    return;
                }
                images = new List<Image>();
                foreach (var rect in rects) {
                   images.Add(rect.GetComponent<Image>()); 
                }
            }
        }
        
        
        [Serializable]
        public class RectTransformSingleSettings: RectTransformSettings{
            [VerticalGroup("Fields/Group")]
            [LabelWidth(60)]
            public RectTransform rect; 
            [VerticalGroup("Fields/Group")]
            [LabelWidth(60)]
            public Image image;
            
            public RectTransformSingleSettings(string header) : base(header) {
            }
            public override void SetSettings() {
                rect.sizeDelta = _rectSize;
                image.color = _color;
                image.sprite = _texture;
            }

            [Button]
            public override void GetImageComponent() {
                if (rect == null) {
                    Debug.Log("Rect is null");
                    return;
                }
                image = rect.GetComponent<Image>();
            }

            
        }
    }
}