﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput
{
	#region FIELDS

	public event System.Action OnKeyAction = delegate { };

	#endregion

	#region PROPERTIES

	public int Id {
		get;
		private set;
	} = -1;

	public List<KeyCode> KeyCodes {
		get;
		private set;
	} = new List<KeyCode>();

	public KeyStateEnum KeyState {
		get;
		private set;
	} = KeyStateEnum.KEY_HOLD;

	public CheckingModeEnum CheckingKeyMode {
		get;
		private set;
	} = CheckingModeEnum.CONJUNCTION;

	public OccurrenceModeEnum OccurrenceMode {
		get;
		private set;
	} = OccurrenceModeEnum.KEY_HAS_OCCUR;

	#endregion

	#region METHODS

	public KeyInput(KeyCode newKeyCode, KeyStateEnum newKeyState, CheckingModeEnum newCheckingKeyMode, System.Action newOnKeyAction, OccurrenceModeEnum newOccurrenceMode = OccurrenceModeEnum.KEY_HAS_OCCUR)
	{
		KeyCodes.Add(newKeyCode);
		KeyState = newKeyState;
		CheckingKeyMode = newCheckingKeyMode;
		OnKeyAction = newOnKeyAction;
		OccurrenceMode = newOccurrenceMode;
	}

	public KeyInput(List<KeyCode> newKeyCode, KeyStateEnum newKeyState, CheckingModeEnum newCheckingKeyMode, System.Action newOnKeyAction, OccurrenceModeEnum newOccurrenceMode = OccurrenceModeEnum.KEY_HAS_OCCUR)
	{
		KeyCodes.AddRange(newKeyCode);
		KeyState = newKeyState;
		CheckingKeyMode = newCheckingKeyMode;
		OnKeyAction = newOnKeyAction;
		OccurrenceMode = newOccurrenceMode;
	}

	public void SetId(int newId)
	{
		Id = newId;
	}

	public void HandleOnKeyAction()
	{
		OnKeyAction();
	}

	public void CheckKey()
	{
		switch (KeyState)
		{
			case KeyInput.KeyStateEnum.KEY_HOLD:
				HandleKey(HandleKeyHold);
				break;
			case KeyInput.KeyStateEnum.KEY_PRESSED_DOWN:
				HandleKey(HandleKeyPressedDown);
				break;
			case KeyInput.KeyStateEnum.KEY_RELEASED:
				HandleKey(HandleKeyReleased);
				break;
		}
	}

	private void HandleKey(System.Func<KeyCode, bool> checkKey)
	{
		switch (CheckingKeyMode)
		{
			case CheckingModeEnum.CONJUNCTION:
				CheckConjunction(checkKey);
				break;
			case CheckingModeEnum.DISJUNCTION:
				CheckDisjunction(checkKey);
				break;
		}
	}

	private void CheckDisjunction(Func<KeyCode, bool> checkKey)
	{
		for (int i = 0; i < KeyCodes.Count; i++)
		{
			switch (OccurrenceMode)
			{
				case OccurrenceModeEnum.KEY_HAS_OCCUR:
					if (checkKey(KeyCodes[i]) == true)
					{
						HandleOnKeyAction();
						return;
					}
					break;
				case OccurrenceModeEnum.KEY_HAS_NOT_OCCUR:
					if (checkKey(KeyCodes[i]) == false)
					{
						HandleOnKeyAction();
						return;
					}
					break;
			}
		}
	}

	private void CheckConjunction(Func<KeyCode, bool> checkKey)
	{
		for (int i = 0; i < KeyCodes.Count; i++)
		{
			switch (OccurrenceMode)
			{
				case OccurrenceModeEnum.KEY_HAS_OCCUR:
					if (checkKey(KeyCodes[i]) == false)
					{
						return;
					}
					break;
				case OccurrenceModeEnum.KEY_HAS_NOT_OCCUR:
					if (checkKey(KeyCodes[i]) == true)
					{
						return;
					}
					break;
			}
		}

		HandleOnKeyAction();
	}

	private bool HandleKeyHold(KeyCode keyCode)
	{
		return Input.GetKey(keyCode);
	}

	private bool HandleKeyPressedDown(KeyCode keyCode)
	{
		return Input.GetKeyDown(keyCode);
	}

	private bool HandleKeyReleased(KeyCode keyCode)
	{
		return Input.GetKeyUp(keyCode);
	}

	#endregion

	#region ENUMS

	public enum KeyStateEnum
	{
		KEY_HOLD,
		KEY_PRESSED_DOWN,
		KEY_RELEASED
	}

	public enum CheckingModeEnum
	{
		CONJUNCTION,
		DISJUNCTION
	}

	public enum OccurrenceModeEnum
	{
		KEY_HAS_OCCUR,
		KEY_HAS_NOT_OCCUR
	}

	#endregion
}
