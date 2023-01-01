using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UtilityScripts {
    public class AgePanel: MonoBehaviour {
        [SerializeField] List<Button> _numberButtons;
        [SerializeField] Button _backspaceButton;
        [SerializeField] Button _continueButton;
        [SerializeField] Text _ageText;
        [SerializeField] List<string> _forbiddenSymbols;

        int _maxSymbolsCount = 2;
        string _currentSymbols = "";

        int _symbolsCount = 0;
        

        void Start() {
            
            _ageText.text = _currentSymbols;
           // _ageText.font = TextLoader.Instance.Font;
            int i = 0;
            foreach (var btn in _numberButtons) {
                var text =  btn.GetComponentInChildren<Text>();
              //  text.font = TextLoader.Instance.Font;
               text.text = i.ToString();
                btn.onClick.AddListener(delegate { AddAgeSymbol(btn.GetComponentInChildren<Text>().text); });
                i++;
            }
            
            _backspaceButton.onClick.AddListener(RemoveSymbol);
            _continueButton.onClick.AddListener(SetAge);
        }


        void AddAgeSymbol(string symbol) {
            if (_symbolsCount < _maxSymbolsCount) {
                _currentSymbols += symbol;

                if (_currentSymbols.Length > 1) {
                    _ageText.text = _currentSymbols[0] + "  " + _currentSymbols[1];
                }
                else {
                    _ageText.text = _currentSymbols;
                }

                _symbolsCount++;
            }
        }

        void RemoveSymbol() {
            if (_symbolsCount > 0) {
              _currentSymbols = _currentSymbols.Remove(_currentSymbols.Length - 1);
                _ageText.text = _currentSymbols;
                _symbolsCount--;
            }
        }

        void SetAge() {
            if(_forbiddenSymbols.All(s => s != _currentSymbols))  {
                if (_currentSymbols != String.Empty) {
                    AppPolicyManager.Instance.SetUserAge(int.Parse(_currentSymbols));
                    PanelManager.Instance.CloseAgePanel();
                }
            }
        }
    }
}