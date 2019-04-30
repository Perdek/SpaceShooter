using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
	#region FIELDS

	private const float MIN_SPEED_TO_SET = 0;
	private const float MAX_SPEED_TO_SET = 100;

	[Header("Controllers")]
	[SerializeField]
	private PlayerMovementController playerMovementController = null;
	[SerializeField]
	private PlayerShootingController playerShootingController = null;
	[SerializeField]
	private PlayerColliderController playerColliderController = null;

	[Header("Movement")]
	[SerializeField, Range(MIN_SPEED_TO_SET, MAX_SPEED_TO_SET)]
	private float playerMaxSpeed = 1;
	[SerializeField]
	private float playerAccelerationFactory = 1;
	[SerializeField]
	private float playerBrakingFactory = 1;

	#endregion

	#region PROPERTIES

	public PlayerMovementController PlayerMovementController => playerMovementController;
	public PlayerShootingController PlayerShootingController => playerShootingController;
	public PlayerColliderController PlayerColliderController => playerColliderController;
	public float PlayerMaxSpeed => playerMaxSpeed;
	public float PlayerAccelerationFactory => playerAccelerationFactory;
	public float PlayerBrakingFactory => playerBrakingFactory;

	#endregion

	#region METHODS

	public void Initialize()
	{
		PlayerMovementController.Initialize(PlayerAccelerationFactory, PlayerBrakingFactory, PlayerMaxSpeed);
		PlayerShootingController.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
