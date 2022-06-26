using Managers.GameManagers;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.WaitingRoom
{
    public class WeaponInformation : SpawnableElement<Weapon>
    {
        #region MEMBERS

        [SerializeField] private TMPro.TMP_Text weaponNameText = default;
        [SerializeField] private TMPro.TMP_Text upgradeWeaponCostText = default;
        [SerializeField] private List<Toggle> weaponsProgressLevelToggles = new List<Toggle>();
        [SerializeField] private Button upgradeButton = default;

        private IPlayerManager playerManager;

        #endregion

        #region PROPERTIES

        public override Weapon ValueReference { get; set; }

        #endregion

        #region METHODS

        [Inject]
        public void InjectDependencies(IPlayerManager playerManager)
        {
            this.playerManager = playerManager;
        }

        public override void AttachEvents()
        {
            upgradeButton.onClick.AddListener(UpgradeWeapon);
            playerManager.PlayerStatisticsController.MoneyPoints.OnValueSet += RefreshOnUpgradeWeapon;
            playerManager.PlayerStatisticsController.MoneyPoints.OnAddValue += RefreshOnUpgradeWeapon;
            playerManager.PlayerStatisticsController.MoneyPoints.OnRemoveValue += RefreshOnUpgradeWeapon;
            ValueReference.WeaponLevel.OnAddValue += RefreshOnUpgradeWeapon;
        }

        public override void DetachEvents()
        {
            upgradeButton.onClick.RemoveListener(UpgradeWeapon);
            playerManager.PlayerStatisticsController.MoneyPoints.OnValueSet -= RefreshOnUpgradeWeapon;
            playerManager.PlayerStatisticsController.MoneyPoints.OnAddValue -= RefreshOnUpgradeWeapon;
            playerManager.PlayerStatisticsController.MoneyPoints.OnRemoveValue -= RefreshOnUpgradeWeapon;
            ValueReference.WeaponLevel.OnAddValue -= RefreshOnUpgradeWeapon;
        }

        public override GameObject GetGameObject()
        {
            return gameObject;
        }

        public override void HandleDestroy()
        {

        }

        public override void RefreshElement()
        {
            weaponNameText.text = ValueReference.WeaponInformation.WeaponName;
            upgradeWeaponCostText.text = ValueReference.GetCurrentUpgradingCostCurve().ToString();

            RefreshTogglesLevelInformation();

            SetUpgradeButton();
        }

        private void SetUpgradeButton()
        {
            if (ValueReference.IsLastLevel() == true)
            {
                return;
            }

            bool canPlayerAffordUpgradingWeapon = playerManager.PlayerStatisticsController.CurrentMoneyPoints >= ValueReference.GetCurrentUpgradingCostCurve();
            upgradeButton.interactable = canPlayerAffordUpgradingWeapon;
        }

        private void RefreshTogglesLevelInformation()
        {
            for (int i = 0; i < weaponsProgressLevelToggles.Count; i++)
            {
                weaponsProgressLevelToggles[i].isOn = ValueReference.WeaponLevel.Value > i;
            }
        }

        private void UpgradeWeapon()
        {
            ValueReference.UpgradeWeapon();
        }

        private void RefreshOnUpgradeWeapon(int value)
        {
            RefreshElement();
        }

        #endregion
    }
}