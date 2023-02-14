using Managers.GameManagers;
using Zenject;

public class TopLeftPanelModel : Model
{
    #region MEMBERS

    private IPlayerManager playerManager;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IPlayerManager playerManager)
    {
        this.playerManager = playerManager;
    }

    public IntValue GetMoneyParameter()
    {
        return playerManager.PlayerStatisticsController.MoneyPoints;
    }

    public IntValue GetScoreParameter()
    {
        return playerManager.PlayerStatisticsController.ScorePoints;
    }

    public WeaponValue GetWeaponParameter()
    {
        return playerManager.PlayerShootingController.ActiveWeapon;
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
