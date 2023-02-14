using System;

namespace Managers.LevelManagers
{
    public class LevelEventsCommunicator
    {
        #region EVENTS

        public event Action OnLevelStart = delegate { };
        public event Action OnLevelEnd = delegate { };
        public event Action OnRequestLevelEnd = delegate { };
        public event Action OnRequestLevelStart = delegate { };
        public event Func<bool> IsLevelEnded;

        #endregion

        #region METHODS

        public void NotifyOnLevelStart()
        {
            OnLevelStart();
        }

        public void NotifyOnLevelEnd()
        {
            OnLevelEnd();
        }

        public void NotifyOnRequestLevelEnd()
        {
            OnRequestLevelEnd();
        }
        
        public void NotifyOnRequestLevelStart()
        {
            OnRequestLevelStart();
        }

        public bool NotifyOnIsLevelEnded()
        {
            return IsLevelEnded == null || IsLevelEnded();
        }

        #endregion
    }
}