using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using UnityEngine;

namespace Game {
    public class Creator: DataHolderInjection {
        NumbersJSON _numbersJson;
        ModificationsJSON _modificationsJson;
        EffectsJSON _effectsJson;
        CardsJSON _cardsJson;
        EventsJSON _eventsJson;

        
        List<Number> _numbers;
        List<ModificationBase> _modifications;
        List<Effect> _effects;
        List<Card> _cards;
        List<RandomEvent> _events;
        
        public void LoadDatas(JSONDataPathsContainer dataPathsContainer) {
            _numbersJson.Load(dataPathsContainer.numbersPath);
            _modificationsJson.Load(dataPathsContainer.modificationsPath);
            _effectsJson.Load(dataPathsContainer.effectsPath);
            _cardsJson.Load(dataPathsContainer.cardsPath);
            _eventsJson.Load(dataPathsContainer.randomEventsPath);
        }

       public void Create() {
           _numbers = new List<Number>();
           _modifications = new List<ModificationBase>();
           _effects = new List<Effect>();
           _cards = new List<Card>();
           _events = new List<RandomEvent>();
           CreateNumbers();
           _holder.PassNumbers(_numbers, _numbersJson.NumberJsonDatas);
           CreateModifications(); 
           _holder.PassModifications(_modifications, _modificationsJson.ModificationJsonDatas);
           CreateEffects();
           _holder.PassEffects(_effects, _effectsJson.EffectJsonDatas);
           CreateCards();
           _holder.PassCards(_cards, _cardsJson.CardJsonDatas);
           CreateEvents();
           _holder.PassEvent(_events, _eventsJson.EventsJsonDatas);
       }
       
       void CreateNumbers() {
           foreach (var data in _numbersJson.NumberJsonDatas) {
               _numbers.Add(new Number(data.name, data.initValue, data.minValue, data.maxValue, data.formula));
           }
       }

       void CreateModifications() {
           foreach (var data in _modificationsJson.ModificationJsonDatas) {
               ModificationBase modification;
               switch (data.type) {
                   case "basic":
                       modification = new ModificationBase(data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula);
                       break;
                   case "pending":
                       modification = new ModificationPending(data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula, data.turnsToComplete);
                       break;
                   case "mission":
                       modification = new ModificationMission(data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula, data.turnsToComplete, data.successChanceFormula);
                       break;
                   case "static":
                       modification = new ModificationStatic(data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula);
                       break;
                   default:
                       Debug.LogError("Loaded modification with incorrect data!");
                       modification = new ModificationBase("INCORRECT", _holder.GetNumber("INCORRECT"), "-1");
                       break;
               }
               _modifications.Add(modification);
           }
       }

       void CreateEffects() {
           foreach (var data in _effectsJson.EffectJsonDatas) {
               var listOfModifications = new List<IModification>();

               foreach (var modificationName in data.modificationsName) {
                   listOfModifications.Add( _holder.GetModificationByName(modificationName));
               }
               _effects.Add(new Effect(_holder.GetNumber(data.countNumberName), listOfModifications, data.countNumberName,
                   data.initTurns, _holder.GetNumber(data.turnModificationNumberName)));
           }
       }
       
       void CreateCards() {
           foreach (var data in _cardsJson.CardJsonDatas) {
               var initCosts = new Dictionary<Number, float>();
               foreach (var initCost in data.initCosts) {
                   initCosts.Add(_holder.GetNumber(initCost.Key), initCost.Value);
               }
               _cards.Add(new Card(_holder.GetNumber(data.effectCountNumberName), initCosts, 
                   new Tuple<Number, float>(_holder.GetNumber(data.availabilityNumberName), data.availabilityParameter)));
           }
       }

       void CreateEvents() {
           foreach (var data in _eventsJson.EventsJsonDatas) {
               _events.Add(new RandomEvent(data.effectCountNumberName, _holder.GetNumber(data.effectCountNumberName), 
                   _holder.GetNumber(data.probabilityNumberName)));
           }
       }
       
    }
}