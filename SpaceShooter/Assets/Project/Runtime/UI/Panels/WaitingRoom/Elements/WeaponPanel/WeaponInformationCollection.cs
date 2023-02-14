using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI.WaitingRoom
{
    [System.Serializable]
    public class WeaponInformationCollection : SpawnableCollection<WeaponInformation, Weapon>
    {
        #region MEMBERS

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void InjectDependencies(DiContainer diContainer)
        {
            base.InjectDependencies(diContainer);
        }

        #endregion
    }
}