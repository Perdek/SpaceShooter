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

	#endregion

	#region METHODS

	public override void Initialize()
	{
		PlayerMainController.Initialize();
	}

	public (int, int) GetPlayerStatistics()
	{
		return (PlayerMainController.PlayerStatisticsController.HealthPoints, PlayerMainController.PlayerStatisticsController.ShieldsPoints);
	}

	#endregion

	#region ENUMS

	#endregion
}
