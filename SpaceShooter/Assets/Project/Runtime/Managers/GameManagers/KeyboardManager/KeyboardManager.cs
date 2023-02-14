using System;
using System.Collections.Generic;
using UnityEngine;

namespace Managers.GameManagers
{
	public class KeyboardManager : IKeyboardManager
	{
		#region FIELDS

		private List<KeyInput> _keysInputs = new List<KeyInput>();

		#endregion

		#region PROPERTIES

		#endregion

		#region METHODS

		public void CheckKeys()
		{
			for (int i = 0; i < _keysInputs.Count; i++)
			{
				_keysInputs[i].CheckKey();
			}
		}

		public Guid AddKey(KeyCode newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR)
		{
			KeyInput newKeyInput = new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrenceMode);
			newKeyInput.SetId(Guid.NewGuid());
			_keysInputs.Add(newKeyInput);

			return newKeyInput.Id;
		}

		public Guid AddKey(List<KeyCode> newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrenceMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCUR)
		{
			KeyInput newKeyInput = new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrenceMode);
			newKeyInput.SetId(Guid.NewGuid());
			_keysInputs.Add(newKeyInput);

			return newKeyInput.Id;
		}

		public void RemoveKey(Guid idToRemove)
		{
			for (int i = _keysInputs.Count - 1; i >= 0; i--)
			{
				if (_keysInputs[i].Id == idToRemove)
				{
					_keysInputs.RemoveAt(i);
					return;
				}
			}
		}

		#endregion

		#region ENUMS

		#endregion
	}
}
