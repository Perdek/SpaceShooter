using UnityEngine;
using Zenject;

namespace Managers.GameManagers
{
    public class UpdateManager : IUpdateManager, ITickable, IFixedTickable
    {
        #region FIELDS

        public event System.Action OnUpdateInputInformation = delegate { };
        public event System.Action OnDataChange = delegate { };
        public event System.Action OnUpdatePhysic = delegate { };
        public event System.Action OnUpdateView = delegate { };

        #endregion

        #region PROPERTIES

        #endregion

        #region ZENJECT_METHODS

        public void Tick()
        {
            HandleOnUpdateInputInformation();
        }

        public void FixedTick()
        {
            HandleOnDataChange();
            HandleOnUpdatePhysic();
            HandleOnUpdateView();
        }

        #endregion

        #region METHODS

        public void PauseTime()
        {
            Time.timeScale = 0;
        }

        public void UnPauseTime()
        {
            Time.timeScale = 1;
        }

        private void HandleOnUpdateInputInformation()
        {
            OnUpdateInputInformation();
        }

        private void HandleOnDataChange()
        {
            OnDataChange();
        }

        private void HandleOnUpdatePhysic()
        {
            OnUpdatePhysic();
        }

        private void HandleOnUpdateView()
        {
            OnUpdateView();
        }

        #endregion

        #region ENUMS

        #endregion
    }
}