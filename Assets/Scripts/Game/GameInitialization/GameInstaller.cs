using Zenject;
using Game.CoreGameplay.Injections;
using UniRx;

namespace Game {
    public class GameInstaller: MonoInstaller {
            public override void InstallBindings() {
                Container.Bind<IDataHolder>().To<Holder>().AsSingle();
                Container.Bind<GRES_Solver>().To<GRES_Solver>().AsSingle();
                Container.Bind<CompositeDisposable>().To<CompositeDisposable>().AsSingle();
            }
    }
}