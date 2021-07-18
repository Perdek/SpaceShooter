using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class StatisticsPanel
    {
        #region MEMBERS

        [SerializeField] private IntValueTMP scoreValue = default;
        [SerializeField] private IntValueTMP moneyValue = default;
        [SerializeField] private IntValueTMP hpValue = default;
        [SerializeField] private IntValueTMP shieldValue = default;
        [SerializeField] private IntValueTMP hpCostValue = default;
        [SerializeField] private IntValueTMP shieldCostValue = default;
        [SerializeField] private Button upgradeHP = default;
        [SerializeField] private Button upgradeShield = default;

        #endregion

        #region PROPERTIES

        private IntValueTMP ScoreValue => scoreValue;
        private IntValueTMP MoneyValue => moneyValue;
        private IntValueTMP HPValue => hpValue;
        private IntValueTMP ShieldValue => shieldValue;
        private Button UpgradeHP => upgradeHP;
        private Button UpgradeShield => upgradeShield;
        private IntValueTMP HpCostValue => hpCostValue;
        private IntValueTMP ShieldCostValue => shieldCostValue;

        #endregion

        #region METHODS

        public void RegisterPlayerStatistics(PlayerStatisticsController playerStatisticsController)
        {
            ScoreValue.RegisterValue(playerStatisticsController.ScorePoints);
            MoneyValue.RegisterValue(playerStatisticsController.MoneyPoints);
            HPValue.RegisterValue(playerStatisticsController.HealthPoints);
            ShieldValue.RegisterValue(playerStatisticsController.ShieldsPoints);
        }

        public void RefreshCosts(ShopCostsInformation shopCostsInformation)
        {
            HpCostValue.text = "Cost: " + shopCostsInformation.HpCost;
            ShieldCostValue.text = "Cost: " + shopCostsInformation.ShieldCost;
        }

        public void AddListenerToUpgradeHP(UnityAction onClick)
        {
            UpgradeHP.onClick.AddListener(onClick);
        }

		public void AddListenerToUpgradeShield(UnityAction onClick)
        {
            UpgradeShield.onClick.AddListener(onClick);
        }

		public void RefreshShieldButtonInteractivity(bool value)
		{
			UpgradeShield.interactable = value;
		}

		public void RefreshHPButtonInteractivity(bool value)
		{
			UpgradeHP.interactable = value;
		}

        #endregion
    }
}
