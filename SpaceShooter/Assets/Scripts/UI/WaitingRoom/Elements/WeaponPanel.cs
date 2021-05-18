using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class WeaponPanel
    {
        #region MEMBERS

        [SerializeField] private TMPro.TMP_Text weaponNameText = default;
        [SerializeField] private TMPro.TMP_Text upgradeWeaponCostText = default;
        [SerializeField] private List<Toggle> weaponsProgressLevelToggles = new List<Toggle>();
        [SerializeField] private Button upgradeButton = default;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        #endregion
    }
}
