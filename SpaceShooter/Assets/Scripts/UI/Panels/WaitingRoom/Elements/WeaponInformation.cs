using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WaitingRoom
{
    public class WeaponInformation : MonoBehaviour
    {
        #region FIELDS

        [SerializeField] private IntValueTMP weaponUpgradeCostText = default;
        [SerializeField] private List<Image> levelImages = new List<Image>();
        [SerializeField] private Button upgradeButton = default;

        #endregion

        #region PROPERTIES

        private IntValueTMP WeaponUpgradeCostText => weaponUpgradeCostText;
        private List<Image> LevelImages => levelImages;
        private Button UpgradeButton => upgradeButton;

        #endregion

        #region METHODS

        #endregion
    }
}
