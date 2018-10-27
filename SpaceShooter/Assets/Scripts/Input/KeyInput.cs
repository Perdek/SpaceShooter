using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput
{
	#region FIELDS

	public event System.Action OnKeyAction = delegate { };

	#endregion

	#region PROPERTIES

	public KeyCode KeyCode {
		get;
		private set;
	}

	public InputMode Mode {
		get;
		private set;
	}

	#endregion

	#region METHODS

	public KeyInput(KeyCode newKeyCode, InputMode newMode, System.Action newOnKeyAction)
	{
		KeyCode = newKeyCode;
		Mode = newMode;
		OnKeyAction = newOnKeyAction;
	}

	public void HandleOnKeyAction()
	{
		OnKeyAction();
	}

	#endregion

	#region ENUMS

	public enum InputMode
	{
		KEY_HOLD,
		KEY_PRESSED_DOWN,
		KEY_RELEASED
	}

	#endregion
}
