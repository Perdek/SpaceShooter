using System;

namespace SceneObjects.Ammo
{
    public class KillingCommunicator
    {
        #region EVENTS

        public event Action<EnemyInformation> OnKillEnemy = delegate { };

        #endregion

        #region METHODS

        public void NotifyOnKillEnemy(EnemyInformation enemyInformation)
        {
            OnKillEnemy(enemyInformation);
        }

        #endregion
    }
}