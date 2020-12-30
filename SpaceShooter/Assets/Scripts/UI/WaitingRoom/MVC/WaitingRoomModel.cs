using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingRoomModel : Model
{
	#region MEMBERS

	[SerializeField]
	private ShopCostsInformation basicHealthCosts = default;

	#endregion

	#region PROPERTIES

	private ShopCostsInformation BasicHealthCosts => basicHealthCosts;

	#endregion

	#region METHODS

	public PlayerStatisticsController GetPlayerStatistics()
	{
		return PlayerManager.Instance.PlayerStatisticsController;
	}

	public void Save()
	{

	}

	public void Ready()
	{
		GameMainManager.Instance.StartNextLevel();
	}

	public void Exit()
	{
		GameMainManager.Instance.OpenMenu();
	}

	public void UpgradeHP()
	{
		if (CanPlayerAffordUpgradingHP() == true)
		{
			PlayerManager.Instance.BuyHP(1, BasicHealthCosts.HpCost);
		}
	}

	public void UpgradeShield()
	{
		if (CanPlayerAffordUpgradingShield() == true)
		{
			PlayerManager.Instance.BuyShield(1, BasicHealthCosts.ShieldCost);
		}
	}

	public bool CanPlayerAffordUpgradingHP()
	{
		return PlayerManager.Instance.PlayerMainController.CanPlayerAffordCost(BasicHealthCosts.HpCost);
	}

	public bool CanPlayerAffordUpgradingShield()
	{
		return PlayerManager.Instance.PlayerMainController.CanPlayerAffordCost(BasicHealthCosts.ShieldCost);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
