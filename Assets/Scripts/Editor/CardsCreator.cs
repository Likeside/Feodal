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
        public int AvailabilityParameter;

        public List<InitCost> InitCosts = new();

        protected override void CreateDataAndLoadTextAsset() {
            
        }

        protected override void FillData() {
            
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