using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Template.UI;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Utilities.OdinEditor {
    
    [ExecuteInEditMode]
    public class TextLoaderEditor: MonoBehaviour {
        
        [SerializeField] List<TextSettingsPlain> _plainTexts;
        [PropertySpace(SpaceBefore = 10, SpaceAfter = 30)]
        [SerializeField] List<TextSettingsMesh> _textMeshes;
        
        [HorizontalGroup("plain")]
        public string plainText;
        [HorizontalGroup("plain")]
        [Button]
        public void AddPlainText() {
            _plainTexts.Add(new TextSettingsPlain(plainText));
            plainText = String.Empty;
        }

        [HorizontalGroup("mesh")]
        public string textMesh;
        [HorizontalGroup("mesh")]
        [Button]
        public void AddTextMesh() {
            _textMeshes.Add(new TextSettingsMesh(textMesh));
            textMesh = String.Empty;
        }

        [VerticalGroup("TextsAndFonts")]
        [Button]
        public void SetTextFontFromTextLoader() {
            var texts = FindObjectsOfType<TextSetter>(true);
            var textsMeshes = FindObjectsOfType<TextSetterMesh>(true);
            var textLoader = FindObjectOfType<TextLoader>();
            textLoader.Set();
            foreach (var text in texts) {
                text.Set(true, textLoader);
            }
            foreach (var text in textsMeshes) {
                text.Set(true, textLoader);
            }
        }
        [HorizontalGroup("TextsAndFonts/Localization")]
        [Button]
        public void SwitchLocalization() {
            var textLoader = FindObjectOfType<TextLoader>();
            textLoader.Set();
            textLoader.SwitchLocalization(localization);
            SetTextFontFromTextLoader();
        }
        [HorizontalGroup("TextsAndFonts/Localization")] 
        [HideLabel]
        public Localization localization;

        [HorizontalGroup("SetFonts")]
        [VerticalGroup("SetFonts/SetFontsTMP")] 
        [LabelWidth(100)]
        public TMP_FontAsset fontTMP;
        [VerticalGroup("SetFonts/SetFontsTMP")]
        [LabelWidth(100)]
        public Transform fontObjsParentTMP;
        [VerticalGroup("SetFonts/SetFontsTMP")] 
        public List<TextMeshProUGUI> groupTextsTMP;
        [VerticalGroup("SetFonts/SetFontsTMP")]
        [Button]
        public void SetFontsTMP() {

            var texts = fontObjsParentTMP.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (var text in groupTextsTMP) {
                text.font = fontTMP;
            }  
            foreach (var text in texts) {
                text.font = fontTMP;
            }
        }
        [VerticalGroup("SetFonts/SetFontsSimple")] 
        [LabelWidth(100)]
        public Font font;

        [VerticalGroup("SetFonts/SetFontsSimple")]
        [LabelWidth(100)]
        public Transform fontObjsParent;
        [VerticalGroup("SetFonts/SetFontsSimple")] 
        public List<Text> groupTexts;
        [VerticalGroup("SetFonts/SetFontsSimple")]
        [Button]
        public void SetFonts() {
            var texts = fontObjsParent.GetComponentsInChildren<Text>();

            foreach (var text in groupTexts) {
                text.font = font;
            }
            foreach (var text in texts) {
                text.font = font;
            }
        }
        
        
        
        void Update() {
            if(EditorApplication.isPlaying ) return;
            foreach (var setting in _plainTexts) {
                setting.SetSettings();
            } 
            foreach (var setting in _textMeshes) {
                setting.SetSettings();
            }
        }
        
        #region TextSettings
        [Serializable]
        public abstract class TextSettings {
            [HideInInspector]
            [SerializeField] string _header;
            [Title("$_header", Bold = true)]

            [Vector2Slider(0, 2566)]
            public Vector2 rectSize;
            [HorizontalGroup("Settings")]
            [HideLabel]
            [LabelWidth(30)]
            public Color color;
            [HorizontalGroup("Settings")]
            [LabelWidth(80)]
            public bool autoSize;
            [HorizontalGroup("Settings")]
            [LabelWidth(50)]
            public AnchorPresets anchor;
            [HorizontalGroup("Settings")]
            [LabelWidth(50)]
            public PivotPresets pivot;
            [HorizontalGroup("Settings")]
            [LabelWidth(50)]
            public int minSize;
            [HorizontalGroup("Settings")]
            [LabelWidth(50)]
            public int maxSize;

            protected TextSettings(string header) {
                _header = header;
            }
            public abstract void SetSettings();
        }
        [Serializable]
        public class TextSettingsPlain: TextSettings {
            public List<Text> texts;
            public TextSettingsPlain(string header) : base(header) {
            }
            public override void SetSettings() {
                foreach (var text in texts) {
                    text.resizeTextForBestFit = autoSize;
                    var rt = text.GetComponent<RectTransform>();
                    if(anchor != AnchorPresets.Ignore) rt.SetAnchor(anchor);
                    if(pivot != PivotPresets.Ignore) rt.SetPivot(pivot);
                    rt.sizeDelta = rectSize;
                    text.resizeTextMinSize = minSize;
                    text.resizeTextMaxSize = maxSize;
                    text.color = color;
                }
            }
        }
        [Serializable]
        public class TextSettingsMesh: TextSettings {
            public List<TextMeshProUGUI> texts;
            public TextSettingsMesh(string header) : base(header) {
            }
            public override void SetSettings() {
                foreach (var text in texts) {
                    text.enableAutoSizing = autoSize;
                    var rt = text.GetComponent<RectTransform>();
                   if(anchor != AnchorPresets.Ignore) rt.SetAnchor(anchor);
                   if(pivot != PivotPresets.Ignore) rt.SetPivot(pivot);
                   rt.sizeDelta = rectSize;
                   text.fontSizeMin = minSize;
                   text.fontSizeMax = maxSize;
                   text.color = color;
                }
            }
        }
        #endregion
    }
}