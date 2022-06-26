using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.GameManagers
{
    public interface IKeyboardManager
    {
        #region PROPERTIES

        public List<KeyInput> KeyInputs { get; }

        #endregion

        #region METHODS

        public void CheckKeys();

        public int AddKey(KeyCode newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR);

        public int AddKey(List<KeyCode> newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR);

        public void RemoveKey(int idToRemove);

        public void Initialize();

        #endregion
    }
}
