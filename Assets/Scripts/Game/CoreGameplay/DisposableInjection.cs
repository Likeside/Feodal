using UniRx;
using Zenject;

namespace Game.CoreGameplay {
    public class DisposableInjection {

        protected CompositeDisposable _disposable;
        
        [Inject]
        public void SetDisposable(CompositeDisposable compositeDisposable) {
            _disposable = compositeDisposable;
        }
    }
}