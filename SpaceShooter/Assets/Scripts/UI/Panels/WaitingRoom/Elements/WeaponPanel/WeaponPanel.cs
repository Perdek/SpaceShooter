using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class WeaponPanel
    {
        #region MEMBERS

        [SerializeField]
        private WeaponInformationCollection weaponInformationCollection = new WeaponInformationCollection();

        #endregion

        #region PROPERTIES

        private WeaponInformationCollection WeaponInformationCollection => weaponInformationCollection;

        #endregion

        #region METHODS

        public void RefreshPanel(List<Weapon> weaponList)
        {
            WeaponInformationCollection.RefreshSection(weaponList);
        }

        public void ClearPanel()
        {
            WeaponInformationCollection.ClearSection();
        }

        #endregion
    }
}
