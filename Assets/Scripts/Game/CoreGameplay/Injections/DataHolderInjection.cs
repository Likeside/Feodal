using PlasticPipe.Server;
using Zenject;

namespace Game.CoreGameplay.Injections {
    public class DataHolderInjection {
        
        protected IDataHolder _holder;


        public DataHolderInjection(IDataHolder holder) {
            _holder = holder;
        }
        /*
        [Inject]
        public void SetDataHolder(IDataHolder holder) {
            _holder = holder;
        }
        */
        
        
    }
}