namespace Managers.GameManagers
{
    public interface IPlayerManager
    {
        #region PROPERTIES

        public PlayerMainController PlayerMainController { get; }
        public PlayerStatisticsController PlayerStatisticsController { get; }
        public PlayerShootingController PlayerShootingController { get; }

		#endregion

		#region METHODS

		public void Initialize();
		public void ReloadPlayer();
		public void BuyHP(int value, int cost);
		public void BuyShield(int value, int cost);

		#endregion
	}
}
