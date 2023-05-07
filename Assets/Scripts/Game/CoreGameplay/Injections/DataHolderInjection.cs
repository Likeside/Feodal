using PlasticPipe.Server;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class DataHolderInjection {
        
        [Inject]
        protected IDataHolder _holder;
        
        
        
        /*
        [Inject]
        public void SetDataHolder(IDataHolder holder) {
            _holder = holder;
        }
        */
        
        
    }
}