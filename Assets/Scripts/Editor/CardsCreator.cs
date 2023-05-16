using System;
using System.Collections.Generic;
using Game;
using Sirenix.OdinInspector;

namespace Editor {
    public class CardsCreator: EntityCreator<CardsCreator, CardsJSON> {
        public static List<string> s_numbersNames;


        [ValueDropdown(nameof(s_numbersNames))]
        public string EffectCountNumberName = s_numbersNames[0];
        [ValueDropdown(nameof(s_numbersNames))]
        public string AvailabilityNumberName = s_numbersNames[0];  
        public float AvailabilityParameter;

        public List<InitCost> InitCosts = new();

        protected override void CreateDataAndLoadTextAsset() {
            LoadTextAsset(s_container.cardsPath);
            _json ??= new CardsJSON();
            _json.Load(s_container.cardsPath);
        }

        protected override void FillData() {
            _jsonData = new CardJSONData() {
                effectCountNumberName = EffectCountNumberName,
                availabilityNumberName = AvailabilityNumberName,
                availabilityParameter =  AvailabilityParameter,
                initCosts = new Dictionary<string, float>()
            };
            foreach (var initCost in InitCosts) {
                ((CardJSONData)_jsonData).initCosts.Add(initCost.Number, initCost.Cost);
            }
        }

        protected override bool IsValid(IJsonData data) {
            
            return true;
        } 
    }

    
    public class InitCost {

        public static List<string> s_numbersNames = CardsCreator.s_numbersNames;

        
        [ValueDropdown(nameof(s_numbersNames))]
        public string Number = s_numbersNames[0];

        public float Cost;
    }
}