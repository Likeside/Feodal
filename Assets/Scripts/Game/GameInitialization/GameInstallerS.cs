using Zenject;
using Game.CoreGameplay.Injections;
using UniRx;

namespace Game {
    public class GameInstallerS: MonoInstaller {
        CompositeDisposable _compositeDisposable;
            public override void InstallBindings() {
                Container.Bind<CompositeDisposable>().To<CompositeDisposable>().AsSingle();
                Container.Bind<IDataHolder>().To<Holder>().AsSingle();
                Container.Bind<GRES_Solver>().To<GRES_Solver>().AsSingle();
            }
    }
}