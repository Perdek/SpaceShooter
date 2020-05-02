using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
	#region FIELDS

	[Header("Controllers")]
	[SerializeField]
	private PlayerMovementController playerMovementController = null;
	[SerializeField]
	private PlayerShootingController playerShootingController = null;
	[SerializeField]
	private PlayerColliderController playerColliderController = null;

	#endregion

	#region PROPERTIES

	public PlayerMovementController PlayerMovementController => playerMovementController;
	public PlayerShootingController PlayerShootingController => playerShootingController;
	public PlayerColliderController PlayerColliderController => playerColliderController;

	#endregion

	#region METHODS

	public void Initialize()
	{
		PlayerMovementController.Initialize();
		PlayerShootingController.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
