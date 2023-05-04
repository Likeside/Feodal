using Zenject;

namespace Game.CoreGameplay.Injections {
    public class IViewDataHolderInjection {
        
        protected IViewDataHolder _holder;
        
        [Inject]
        public void SetImageAssetHolder(IViewDataHolder holder) {
            _holder = holder;
        }
    }
}