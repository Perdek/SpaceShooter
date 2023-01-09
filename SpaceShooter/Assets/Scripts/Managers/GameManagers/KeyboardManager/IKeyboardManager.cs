using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.GameManagers
{
    public interface IKeyboardManager
    {
        #region METHODS

        public void CheckKeys();

        public Guid AddKey(KeyCode newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR);

        public Guid AddKey(List<KeyCode> newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR);

        public void RemoveKey(Guid idToRemove);

        #endregion
    }
}
