using Zenject;
using Game.CoreGameplay.Injections;
using UniRx;
using UnityEngine;

namespace Game {
    public class GameInstallerS: MonoInstaller {
        
            public override void InstallBindings() {
                Debug.Log("Installing bindings");
                /*
                CompositeDisposable compositeDisposable = new CompositeDisposable();
                Holder holder = new Holder();
                GRES_Solver solver = new GRES_Solver();*/
                if(Container == null) Debug.Log("Containernull");
                Container.Bind<CompositeDisposable>().To<CompositeDisposable>().AsSingle();
                Container.Bind<IDataHolder>().To<Holder>().AsSingle();
                Container.Bind<GRES_Solver>().To<GRES_Solver>().AsSingle();
            }
    }
}