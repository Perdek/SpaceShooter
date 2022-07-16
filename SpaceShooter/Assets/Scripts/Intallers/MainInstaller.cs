using UnityEngine;
using Managers.GameManagers;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [SerializeField]
    private GameMainManager gameMainManager;
    [SerializeField]
    private PoolManager poolManager;
    [SerializeField]
    private UpdateManager updateManager;
    [SerializeField]
    private PlayerManager playerManager;

    public override void InstallBindings()
    {
        Container.Bind<IGameMainManager>().To(typeof(GameMainManager)).FromInstance(gameMainManager).AsSingle();
        Container.Bind<IPoolManager>().To(typeof(PoolManager)).FromInstance(poolManager).AsSingle();
        Container.Bind<IUpdateManager>().To(typeof(UpdateManager)).FromInstance(updateManager).AsSingle();
        Container.Bind<IKeyboardManager>().To<KeyboardManager>().AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().AsSingle();
        Container.Bind<IPlayerManager>().To(typeof(PlayerManager)).FromInstance(playerManager).AsSingle();


        Container.Bind<Weapon>().AsTransient();
        Container.Bind<BasePoolObjectsFactory>().AsCached();

        Container.BindFactory<UnityEngine.Object, IBasePoolObject, IBasePoolObject.Factory>().FromFactory<BasePoolObjectsFactory>();
    }
}