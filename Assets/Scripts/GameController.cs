using System;
using System.Collections.Generic;
using System.Linq;
using Game;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using Template.AdsAndAnalytics;
using Template.UI;
using UnityEngine;
using Utilities;
using Zenject;

public class GameController : LocalSingleton<GameController> {
        
        [SerializeField] ButtonManager _buttonManager;
        [SerializeField] JSONDataPathsContainer _dataPathsContainer;
        [SerializeField] DummyDisplay _display;
        
       
        IDataHolder _holder;

        List<Card> _cardsInSlot;

        void Awake() {
                GameInstallerS.Install(new DiContainer());
        }

        void Start() { 
                //TODO: Отписываться в определенный момент игры
                if (_buttonManager.TipButtonActive) _buttonManager.OnTipButtonPressed += TipButtonPressed;
                if (_buttonManager.PauseButtonActive) _buttonManager.OnTipButtonPressed += PauseButtonPressed;
                if (PanelManager.Instance.ElementsActiveness.rateUsPanelActive) ExternalLinksManager.Instance.OnRateUsLinkOpened += RateUsLinkOpened;
                
                Debug.Log("Init creator");
                Creator creator = new Creator();
                Debug.Log("Loading datas");
                creator.LoadDatas(_dataPathsContainer);
                Debug.Log("Creating");
                creator.Create();
                
                _cardsInSlot = new List<Card>();
        }

        void RateUsLinkOpened() {
                Debug.Log("Rate us link opened");
        }

        void PauseButtonPressed() {
                Debug.Log("Pause button pressed");
        }

        void TipButtonPressed() {
                Debug.Log("Tip button pressed");
        }

        public void CardPassedToSlot(string cardName) {
                var card = _holder.Cards.FirstOrDefault(c => c.Name.Equals(cardName));
             _cardsInSlot.Add(card); 
             card.PayCost();
        }

        public void CardRemovedFromSlot(string cardName) {
                var card = _cardsInSlot.FirstOrDefault(c => c.Name.Equals(cardName));
                _cardsInSlot.Remove(card);
                card.RestoreCostBeforeEndOfTurn();
        }

        public void EndTurn() {
                foreach (var card in _cardsInSlot) {
                        card.ApplyAtEndOfTurn();
                }

                foreach (var effect in _holder.Effects) {
                        effect.ApplyAtEndOfTurn();
                }

                foreach (var randomEvent in _holder.RandomEvents) {
                        randomEvent.TryToApplyEvent();
                }
                
                _display.DisplayCards(_holder.Cards);
                _display.DisplayEffects(_holder.Effects);
                _display.DisplayEvents(_holder.RandomEvents);
                _display.DisplayNumbers(_holder.Numbers);
                
        }
}
