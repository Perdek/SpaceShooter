using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseSingletonManager<PlayerManager>
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public PlayerMovementController MovementController {
		get;
		private set;
	} = new PlayerMovementController();

	public PlayerShootingController ShootingController {
		get;
		private set;
	} = new PlayerShootingController();

	#endregion

	#region METHODS

	public void Initialize()
	{
		
	}

	#endregion

	#region ENUMS

	#endregion
}
