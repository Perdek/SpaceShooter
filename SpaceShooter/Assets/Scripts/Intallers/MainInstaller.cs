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
    private PlayerManager playerManager;

    public override void InstallBindings()
    {
        Container.Bind<IGameMainManager>().To(typeof(GameMainManager)).FromInstance(gameMainManager).AsSingle();
        Container.Bind<IPoolManager>().To(typeof(PoolManager)).FromInstance(poolManager).AsSingle();
        Container.Bind<IKeyboardManager>().To<KeyboardManager>().AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().AsSingle();
        Container.Bind<IPlayerManager>().To(typeof(PlayerManager)).FromInstance(playerManager).AsSingle();

        Container.BindInterfacesAndSelfTo<UpdateManager>().AsSingle();
        
        Container.Bind<Weapon>().AsTransient();
        Container.Bind<BasePoolObjectsFactory>().AsCached();
        Container.Bind<PoolObjectsParentFactory>().AsCached();

        Container.BindFactory<UnityEngine.Object, UnityEngine.Transform, IBasePoolObject, IBasePoolObject.Factory>().FromFactory<BasePoolObjectsFactory>();
        Container.BindFactory<UnityEngine.Object, UnityEngine.Transform, PoolObjectsParent, PoolObjectsParent.Factory>().FromFactory<PoolObjectsParentFactory>();
    }
}