using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class PlayerMainController : MonoBehaviour
{
    #region FIELDS

    [FormerlySerializedAs("playerMovementController")] [Header("Controllers")] [SerializeField] private PlayerMovementController _playerMovementController = null;

    [FormerlySerializedAs("playerShootingController")] [SerializeField] private PlayerShootingController _playerShootingController = null;
    [FormerlySerializedAs("playerColliderController")] [SerializeField] private PlayerColliderController _playerColliderController = null;
    [FormerlySerializedAs("playerStatisticsController")] [SerializeField] private PlayerStatisticsController _playerStatisticsController = null;
    [FormerlySerializedAs("playerVisualisationController")] [SerializeField] private PlayerVisualisationController _playerVisualisationController = null;

    private IGameMainManager _gameMainManager;

    #endregion

    #region PROPERTIES

    public PlayerMovementController PlayerMovementController => _playerMovementController;
    public PlayerShootingController PlayerShootingController => _playerShootingController;
    public PlayerColliderController PlayerColliderController => _playerColliderController;
    public PlayerStatisticsController PlayerStatisticsController => _playerStatisticsController;
    public PlayerVisualisationController PlayerVisualisationController => _playerVisualisationController;

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IGameMainManager gameMainManager, IUpdateManager updateManager,
        IKeyboardManager keyboardManager, IInputManager inputManager, IPoolManager poolManager,
        LevelEventsCommunicator levelEventsCommunicator)
    {
        this._gameMainManager = gameMainManager;
        _playerMovementController.InjectDependencies(updateManager, keyboardManager, gameMainManager, inputManager);
        _playerShootingController.InjectDependencies(keyboardManager, updateManager, poolManager,
            levelEventsCommunicator);
    }

    public void Initialize()
    {
        AttachEvents();
        _playerMovementController.Initialize();
        _playerShootingController.Initialize();
    }

    public bool CanPlayerAffordCost(int value)
    {
        return _playerStatisticsController.CurrentMoneyPoints >= value;
    }

    private void AttachEvents()
    {
        _gameMainManager.OnGameStart += AttachInterControllersEvents;
        _gameMainManager.OnGameStart += RefreshView;
        _gameMainManager.OnMainOpen += DetachInterControllersEvents;
        _gameMainManager.OnWaitingOpen += DetachInterControllersEvents;
        _playerStatisticsController.OnPlayerDead += _gameMainManager.GameOver;

        _playerShootingController.AttachEventForUpdateWeapon(_playerStatisticsController.MoneyPoints.RemoveValue);
    }

    private void RefreshView()
    {
        if (_playerStatisticsController.CurrentShieldPoints > 0)
        {
            _playerVisualisationController.TurnOnForceShield();
        }

        _playerShootingController.ClearWeaponsReload();
    }

    private void AttachInterControllersEvents()
    {
        _playerColliderController.OnDamageCollision += _playerStatisticsController.HandleDamage;
        _playerShootingController.OnKillEnemy += _playerStatisticsController.RewardForKill;
        _playerStatisticsController.OnTurnOnForceShield += _playerVisualisationController.TurnOnForceShield;
        _playerStatisticsController.OnTurnOffForceShield += _playerVisualisationController.TurnOffForceShield;
    }

    private void DetachInterControllersEvents()
    {
        _playerColliderController.OnDamageCollision -= _playerStatisticsController.HandleDamage;
        _playerShootingController.OnKillEnemy -= _playerStatisticsController.RewardForKill;
        _playerStatisticsController.OnTurnOnForceShield -= _playerVisualisationController.TurnOnForceShield;
        _playerStatisticsController.OnTurnOffForceShield -= _playerVisualisationController.TurnOffForceShield;
    }

    #endregion

    #region ENUMS

    #endregion
}