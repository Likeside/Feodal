using Zenject;

namespace Game.CoreGameplay.Injections {
    public class DataHolderInjection {
        
        protected IDataHolder _holder;
        
        [Inject]
        public void SetDataHolder(IDataHolder holder) {
            _holder = holder;
        }
        
    }
}