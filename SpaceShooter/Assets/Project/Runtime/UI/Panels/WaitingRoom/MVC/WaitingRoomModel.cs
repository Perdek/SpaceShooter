using Managers.GameManagers;
using UnityEngine;
using Zenject;

public class WaitingRoomModel : Model
{
	#region MEMBERS

	[SerializeField]
	private ShopCostsInformation basicHealthCosts = default;

	private IPlayerManager playerManager;
	private IGameMainManager gameMainManager;

	#endregion

	#region PROPERTIES

	private ShopCostsInformation BasicHealthCosts => basicHealthCosts;

    #endregion

    #region METHODS

    [Inject]
	public void InjectDependencies(IPlayerManager playerManager, IGameMainManager gameMainManager)
    {
		this.playerManager = playerManager;
		this.gameMainManager = gameMainManager;
    }

	public PlayerStatisticsController GetPlayerStatistics()
	{
		return playerManager.PlayerStatisticsController;
	}

	public PlayerShootingController GetPlayerShootingController()
    {
		return playerManager.PlayerShootingController;
    }

	public void Save()
	{

	}

	public void Ready()
	{
		gameMainManager.StartNextLevel();
	}

	public void Exit()
	{
		gameMainManager.OpenMenu();
	}

	public void UpgradeHP()
	{
		if (CanPlayerAffordUpgradingHP() == true)
		{
			playerManager.BuyHP(1, BasicHealthCosts.HpCost);
		}
	}

	public void UpgradeShield()
	{
		if (CanPlayerAffordUpgradingShield() == true)
		{
			playerManager.BuyShield(1, BasicHealthCosts.ShieldCost);
		}
	}

	public bool CanPlayerAffordUpgradingHP()
	{
		return playerManager.PlayerMainController.CanPlayerAffordCost(BasicHealthCosts.HpCost);
	}

	public bool CanPlayerAffordUpgradingShield()
	{
		return playerManager.PlayerMainController.CanPlayerAffordCost(BasicHealthCosts.ShieldCost);
	}

	public ShopCostsInformation GetBasicHealthCosts()
    {
		return BasicHealthCosts;
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
