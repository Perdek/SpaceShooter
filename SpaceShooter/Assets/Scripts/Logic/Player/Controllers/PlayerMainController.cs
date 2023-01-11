using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;
using Zenject;

public class PlayerMainController : MonoBehaviour
{
    #region FIELDS

    [Header("Controllers")]
    [SerializeField]
    private PlayerMovementController playerMovementController = null;
    [SerializeField]
    private PlayerShootingController playerShootingController = null;
    [SerializeField]
    private PlayerColliderController playerColliderController = null;
    [SerializeField]
    private PlayerStatisticsController playerStatisticsController = null;
    [SerializeField]
    private PlayerVisualisationController playerVisualisationController = null;

    private IGameMainManager gameMainManager;

    #endregion

    #region PROPERTIES

    public PlayerMovementController PlayerMovementController => playerMovementController;
    public PlayerShootingController PlayerShootingController => playerShootingController;
    public PlayerColliderController PlayerColliderController => playerColliderController;
    public PlayerStatisticsController PlayerStatisticsController => playerStatisticsController;
    public PlayerVisualisationController PlayerVisualisationController => playerVisualisationController;

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IGameMainManager gameMainManager, IUpdateManager updateManager, IKeyboardManager keyboardManager, IInputManager inputManager, IPoolManager poolManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        this.gameMainManager = gameMainManager;
        playerMovementController.InjectDependencies(updateManager, keyboardManager, gameMainManager, inputManager);
        PlayerShootingController.InjectDependencies(keyboardManager, updateManager, poolManager, levelEventsCommunicator);
    }

    public void Initialize()
    {
        AttachEvents();
        PlayerMovementController.Initialize();
        PlayerShootingController.Initialize();
    }

    public bool CanPlayerAffordCost(int value)
    {
        return PlayerStatisticsController.CurrentMoneyPoints >= value;
    }

    private void AttachEvents()
    {
        gameMainManager.OnGameStart += AttachInterControllersEvents;
        gameMainManager.OnGameStart += RefreshView;
        gameMainManager.OnMainOpen += DetachInterControllersEvents;
        gameMainManager.OnWaitingOpen += DetachInterControllersEvents;
        PlayerStatisticsController.OnPlayerDead += gameMainManager.GameOver;

        PlayerShootingController.AttachEventForUpdateWeapon(PlayerStatisticsController.MoneyPoints.RemoveValue);
    }

    private void RefreshView()
    {
        if (PlayerStatisticsController.CurrentShieldPoints > 0)
        {
            PlayerVisualisationController.TurnOnForceShield();
        }

        PlayerShootingController.ClearWeaponsReload();
    }

    private void AttachInterControllersEvents()
    {
        PlayerColliderController.OnDamageCollision += PlayerStatisticsController.HandleDamage;
        PlayerShootingController.OnKillEnemy += PlayerStatisticsController.RewardForKill;
        PlayerStatisticsController.OnTurnOnForceShield += PlayerVisualisationController.TurnOnForceShield;
        PlayerStatisticsController.OnTurnOffForceShield += PlayerVisualisationController.TurnOffForceShield;
    }

    private void DetachInterControllersEvents()
    {
        PlayerColliderController.OnDamageCollision -= PlayerStatisticsController.HandleDamage;
        PlayerShootingController.OnKillEnemy -= PlayerStatisticsController.RewardForKill;
        PlayerStatisticsController.OnTurnOnForceShield -= PlayerVisualisationController.TurnOnForceShield;
        PlayerStatisticsController.OnTurnOffForceShield -= PlayerVisualisationController.TurnOffForceShield;
    }

    #endregion

    #region ENUMS

    #endregion
}
