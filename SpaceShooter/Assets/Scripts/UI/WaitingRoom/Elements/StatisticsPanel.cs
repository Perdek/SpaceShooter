using System;
using UnityEngine.UI;
using UnityEngine;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class StatisticsPanel
    {
        #region MEMBERS

        [SerializeField] private IntValueTMP scoreValue = default;
        [SerializeField] private IntValueTMP moneyValue = default;

        #endregion

        #region PROPERTIES

        private IntValueTMP ScoreValue => scoreValue;
        private IntValueTMP MoneyValue => moneyValue;

        #endregion

        #region METHODS

        public void RegisterPlayerStatistics(PlayerStatisticsController playerStatisticsController)
        {
            ScoreValue.RegisterValue(playerStatisticsController.ScorePoints);
            MoneyValue.RegisterValue(playerStatisticsController.ScorePoints);
        }

        #endregion
    }
}
