using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseSingletonManager<PlayerManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	[SerializeField]
	private PlayerMovementController playerMovementController;
	[SerializeField]
	private PlayerShootingController playerShootingController;

	#endregion

	#region METHODS

	public PlayerMovementController PlayerMovementController => playerMovementController;
	public PlayerShootingController PlayerShootingController => playerShootingController;

	public void Initialize()
	{
		PlayerMovementController.Initialize();
		PlayerShootingController.Initialize();
	}

	private void InitializeMovement()
	{
		//KeyboardManager.Instance.KeyInputs.Add(new KeyInput(KeyCode.A, KeyInput.InputMode.KEY_HOLD, ));
		//KeyboardManager.Instance.KeyInputs.Add(new KeyInput());
		//KeyboardManager.Instance.KeyInputs.Add(new KeyInput());
		//KeyboardManager.Instance.KeyInputs.Add(new KeyInput());
	}

	private void InitializeShooting()
	{
		//KeyboardManager.Instance.KeyInputs.Add(new KeyInput(KeyCode.Space, KeyInput.InputMode.KEY_HOLD, ));
	}

	#endregion

	#region ENUMS

	#endregion
}
