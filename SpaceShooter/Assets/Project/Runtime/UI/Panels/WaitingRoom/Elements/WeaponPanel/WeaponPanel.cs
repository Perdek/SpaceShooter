using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

        public void InjectDependencies(DiContainer diContainer)
        {
            weaponInformationCollection.InjectDependencies(diContainer);
        }

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
