using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardManager : BaseSingletonManager<KeyboardManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public List<KeyInput> KeyInputs {
		get;
		private set;
	} = new List<KeyInput>();

	#endregion

	#region METHODS

	public void CheckKeys()
	{
		for (int i = 0; i < KeyInputs.Count; i++)
		{
			KeyInputs[i].CheckKey();
		}
	}

	public void AddKey(KeyCode newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrencyMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCURE)
	{
		KeyInputs.Add(new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrencyMode));
	}

	public void AddKey(List<KeyCode> newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrencyMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCURE)
	{
		KeyInputs.Add(new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrencyMode));
	}

	public override void Initialize()
	{
		base.Initialize();
		UpdateManager.Instance.OnUpdateInputInformation += CheckKeys;
	}

	#endregion

	#region ENUMS

	#endregion
}
