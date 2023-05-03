using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.CoreGameplay.Effect {
    public class DisposableMonoInjection: MonoBehaviour {
        
        protected CompositeDisposable _disposable;
        
        [Inject]
        public void SetDisposable(CompositeDisposable compositeDisposable) {
            _disposable = compositeDisposable;
        }
        
        void OnDestroy() {
            _disposable.Dispose();
        }
    }
}