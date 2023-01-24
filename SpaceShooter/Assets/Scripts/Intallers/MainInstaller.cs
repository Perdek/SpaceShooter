using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class MainInstaller : MonoInstaller
{
    [FormerlySerializedAs("gameMainManager")] [SerializeField] private GameMainManager _gameMainManager;
    [FormerlySerializedAs("poolManager")] [SerializeField] private PoolManager _poolManager;
    [FormerlySerializedAs("playerManager")] [SerializeField] private PlayerManager _playerManager;

    public override void InstallBindings()
    {
        Container.Bind<IGameMainManager>().To(typeof(GameMainManager)).FromInstance(_gameMainManager).AsSingle();
        Container.Bind<IKeyboardManager>().To<KeyboardManager>().AsSingle();
        Container.Bind<IInputManager>().To<InputManager>().AsSingle();
        Container.Bind<IPlayerManager>().To(typeof(PlayerManager)).FromInstance(_playerManager).AsSingle();
        Container.Bind<LevelEventsCommunicator>().AsSingle();

        Container.BindInterfacesAndSelfTo<PoolManager>().FromInstance(_poolManager).AsSingle();
        Container.BindInterfacesAndSelfTo<UpdateManager>().AsSingle();

        Container.Bind<Weapon>().AsTransient();
        Container.Bind<BasePoolObjectsFactory>().AsCached();
        Container.Bind<PoolObjectsParentFactory>().AsCached();

        Container.BindFactory<Object, Transform, IBasePoolObject, IBasePoolObject.Factory>()
            .FromFactory<BasePoolObjectsFactory>();
        Container.BindFactory<Object, Transform, PoolObjectsParent, PoolObjectsParent.Factory>()
            .FromFactory<PoolObjectsParentFactory>();
    }
}