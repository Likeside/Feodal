using System;
using System.Collections.Generic;
using Game.CoreGameplay.Effect;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game {
    public class Creator {
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


        CompositeDisposable _disposable;
        IDataHolder _holder;
        GRES_Solver _solver;
        
        public void LoadDatas(JSONDataPathsContainer dataPathsContainer) {

            _numbersJson = new NumbersJSON();
            _modificationsJson = new ModificationsJSON();
            _effectsJson = new EffectsJSON();
            _cardsJson = new CardsJSON();
            _eventsJson = new EventsJSON();
            //TODO: заменить на дженерик, грузить при инициализации класса
            
            Debug.Log("Loading numbers");
            _numbersJson.Load(dataPathsContainer.numbersPath);
            Debug.Log("Loading mods");
            _modificationsJson.Load(dataPathsContainer.modificationsPath);
            Debug.Log("Loading effects");
            _effectsJson.Load(dataPathsContainer.effectsPath);
            Debug.Log("Loading cards");
            _cardsJson.Load(dataPathsContainer.cardsPath);
            Debug.Log("Loading events");
            _eventsJson.Load(dataPathsContainer.randomEventsPath);
        }

       public void Create(CompositeDisposable disposable, GRES_Solver solver, IDataHolder holder) {
           _disposable = disposable;
           _solver = solver;
           _holder = holder;
           
           _numbers = new List<Number>();
           _modifications = new List<ModificationBase>();
           _effects = new List<Effect>();
           _cards = new List<Card>();
           _events = new List<RandomEvent>();
           CreateNumbers();
           if(_holder == null) Debug.Log("Holder null");
           _holder.PassNumbers(_numbers, _numbersJson.jsonDatas);
           CreateModifications(); 
           _holder.PassModifications(_modifications, _modificationsJson.jsonDatas);
           CreateEffects();
           _holder.PassEffects(_effects, _effectsJson.jsonDatas);
           CreateCards();
           _holder.PassCards(_cards, _cardsJson.jsonDatas);
           CreateEvents();
           _holder.PassEvent(_events, _eventsJson.jsonDatas);


           foreach (var number in _numbers) {
               number.Initialize();
           }

           foreach (var mod in _modifications) {
               mod.Initialize();
           }
       }
       
       void CreateNumbers() {
           foreach (var data in _numbersJson.jsonDatas) {
               _numbers.Add(new Number(_disposable, _solver, _holder,data.name, data.initValue, data.minValue, data.maxValue, data.formula));
           }
       }

       void CreateModifications() {
           foreach (var data in _modificationsJson.jsonDatas) {
               ModificationBase modification;
               switch (data.type) {
                   case "basic":
                       modification = new ModificationBase(_disposable, _solver, _holder, data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula);
                       break;
                   case "pending":
                       modification = new ModificationPending(_disposable, _solver, _holder,data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula, data.turnsToComplete);
                       break;
                   case "mission":
                       modification = new ModificationMission(_disposable, _solver, _holder,data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula, data.turnsToComplete, data.successChanceFormula);
                       break;
                   case "static":
                       modification = new ModificationStatic(_disposable, _solver, _holder,data.name, _holder.GetNumber(data.numberToModifyName),
                           data.modificationFormula);
                       break;
                   default:
                       Debug.LogError("Loaded modification with incorrect data!");
                       modification = new ModificationBase(_disposable, _solver, _holder,"INCORRECT", _holder.GetNumber("INCORRECT"), "-1");
                       break;
               }
               _modifications.Add(modification);
           }
       }

       void CreateEffects() {
           foreach (var data in _effectsJson.jsonDatas) {
               var listOfModifications = new List<IModification>();

               foreach (var modificationName in data.modificationsName) {
                   listOfModifications.Add( _holder.GetModificationByName(modificationName));
               }
               _effects.Add(new Effect( _disposable, _holder.GetNumber(data.countNumberName), listOfModifications, data.countNumberName,
                   data.initTurns, _holder.GetNumber(data.turnModificationNumberName), data.type));
           }
       }
       
       void CreateCards() {
           foreach (var data in _cardsJson.jsonDatas) {
               var initCosts = new Dictionary<Number, float>();
               foreach (var initCost in data.initCosts) {
                   initCosts.Add(_holder.GetNumber(initCost.Key), initCost.Value);
               }
               _cards.Add(new Card(_disposable, _solver, _holder,_holder.GetNumber(data.effectCountNumberName), initCosts, 
                   new Tuple<Number, float>(_holder.GetNumber(data.availabilityNumberName), data.availabilityParameter)));
           }
       }

       void CreateEvents() {
           foreach (var data in _eventsJson.jsonDatas) {
               _events.Add(new RandomEvent(data.effectCountNumberName, _holder.GetNumber(data.effectCountNumberName), 
                   _holder.GetNumber(data.probabilityNumberName)));
           }
       }

    }
}