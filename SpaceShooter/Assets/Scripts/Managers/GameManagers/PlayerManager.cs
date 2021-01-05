using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseMonoBehaviourSingletonManager<PlayerManager>
{
	#region FIELDS

	[SerializeField]
	private PlayerMainController playerMainController = null;

	#endregion

	#region PROPERTIES

	public PlayerMainController PlayerMainController => playerMainController;

	public PlayerStatisticsController PlayerStatisticsController => PlayerMainController.PlayerStatisticsController;
	public PlayerShootingController PlayerShootingController => PlayerMainController.PlayerShootingController;
	private PlayerMovementController PlayerMovementController => PlayerMainController.PlayerMovementController;

	#endregion

	#region METHODS

	public override void Initialize()
	{
		PlayerMainController.Initialize();
	}

	public void ReloadPlayer()
	{
		PlayerStatisticsController.ReloadStatistics();
		PlayerMovementController.ResetPosition();
	}

	public void BuyHP(int value, int cost)
	{
		PlayerStatisticsController.MoneyPoints.RemoveValue(cost);
		PlayerStatisticsController.HealthPoints.AddValue(value);
	}

	public void BuyShield(int value, int cost)
	{
		PlayerStatisticsController.MoneyPoints.RemoveValue(cost);
		PlayerStatisticsController.AddNewShield(value);
	}

	#endregion

	#region ENUMS

	#endregion
}
