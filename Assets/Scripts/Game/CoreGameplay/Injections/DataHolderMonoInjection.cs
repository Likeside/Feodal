using UnityEngine;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class DataHolderMonoInjection: MonoBehaviour {
        
        protected IDataHolder _holder;
        

        [Inject]
        public void SetHolder(IDataHolder holder) {
            _holder = holder;
        }
    }
}