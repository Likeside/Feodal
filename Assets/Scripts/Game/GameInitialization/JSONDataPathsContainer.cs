using System.Collections.Generic;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "JSONDataPathsContainer", menuName = "Configs/JSONDataPathsContainer", order = 5)]

    public class JSONDataPathsContainer: ScriptableObject {

        public string numbersPath;
        public string modificationsPath;
        public string effectsPath;
        public string cardsPath;
        public string randomEventsPath;

    }
}