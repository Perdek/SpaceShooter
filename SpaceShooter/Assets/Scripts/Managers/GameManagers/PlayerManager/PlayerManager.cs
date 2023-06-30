using UnityEngine;

namespace Managers.GameManagers
{
	public class PlayerManager : MonoBehaviour, IPlayerManager
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

		public void Initialize()
		{
			PlayerMainController.Initialize();
		}

		public void ReloadPlayer()
		{
			PlayerStatisticsController.ReloadStatistics();
			PlayerMovementController.ResetPosition();
			PlayerShootingController.ResetShooting();
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
}
