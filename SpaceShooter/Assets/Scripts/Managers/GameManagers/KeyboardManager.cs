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
	}

	#endregion

	#region METHODS

	public void Initialize()
	{

	}

	public void CheckKeys()
	{
		for (int i = 0; i < KeyInputs.Count; i++)
		{
			CheckKey(KeyInputs[i]);
		}
	}

	private void CheckKey(KeyInput keyInput)
	{
		switch (keyInput.Mode)
		{
			case KeyInput.InputMode.KEY_HOLD:
				HandleKeyHold(keyInput);
				break;
			case KeyInput.InputMode.KEY_PRESSED_DOWN:
				HandleKeyPressedDown(keyInput);
				break;
			case KeyInput.InputMode.KEY_RELEASED:
				HandleKeyReleased(keyInput);
				break;
		}
	}

	private void HandleKeyHold(KeyInput keyInput)
	{
		if (Input.GetKey(keyInput.KeyCode) == true)
		{
			keyInput.HandleOnKeyAction();
		}
	}

	private void HandleKeyPressedDown(KeyInput keyInput)
	{
		if (Input.GetKeyDown(keyInput.KeyCode) == true)
		{
			keyInput.HandleOnKeyAction();
		}
	}

	private void HandleKeyReleased(KeyInput keyInput)
	{
		if (Input.GetKeyUp(keyInput.KeyCode) == true)
		{
			keyInput.HandleOnKeyAction();
		}
	}

	#endregion

	#region ENUMS

	#endregion
}
