using UniRx;
using UnityEngine;
using Zenject;

namespace Game.CoreGameplay {
    public class DisposableInjection {

        protected CompositeDisposable _disposable;
        
        [Inject]
        public void SetDisposable(CompositeDisposable compositeDisposable) {
            bool compositeDisposableNull = compositeDisposable == null;
            Debug.Log("Setting disposable, is null:" + compositeDisposableNull);
        
            _disposable = compositeDisposable;
        }
    }
}