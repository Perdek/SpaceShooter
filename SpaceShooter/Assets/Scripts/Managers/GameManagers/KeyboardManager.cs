using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class KeyboardManager : BaseSingletonManager<KeyboardManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public List<KeyInput> KeyInputs {
		get;
		private set;
	} = new List<KeyInput>();

	private int FreeId {
		get;
		set;
	} = 0;

	#endregion

	#region METHODS

	public void CheckKeys()
	{
		for (int i = 0; i < KeyInputs.Count; i++)
		{
			KeyInputs[i].CheckKey();
		}
	}

	public int AddKey(KeyCode newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrencyMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCURE)
	{
		KeyInput newKeyInput = new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrencyMode);
		newKeyInput.SetId(FreeId++);
		KeyInputs.Add(newKeyInput);

		return newKeyInput.Id;
	}

	public int AddKey(List<KeyCode> newKeyCode, Action newOnKeyAction, KeyInput.KeyStateEnum newInputMode = KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum newCheckingMode = KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum newOccurrencyMode = KeyInput.OccurrenceModeEnum.KEY_HAS_OCCURE)
	{
		KeyInput newKeyInput = new KeyInput(newKeyCode, newInputMode, newCheckingMode, newOnKeyAction, newOccurrencyMode);
		newKeyInput.SetId(FreeId++);
		KeyInputs.Add(newKeyInput);

		return newKeyInput.Id;
	}

	public void RemoveKey(int idToRemove)
	{
		for (int i = KeyInputs.Count - 1; i >= 0; i--)
		{
			if (KeyInputs[i].Id == idToRemove)
			{
				KeyInputs.RemoveAt(i);
				return;
			}
		}
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
