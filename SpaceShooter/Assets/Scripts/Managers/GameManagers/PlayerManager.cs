using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseMonoBehaviourSingletonManager<PlayerManager>
{
	#region FIELDS

	[SerializeField]
	private PlayerMainController playerMainController = null;

	#endregion

	#region PROPERTIES

	public PlayerMainController PlayerMainController => playerMainController;

	#endregion

	#region METHODS

	public void Initialize()
	{
		PlayerMainController.Initialize();
	}

	#endregion

	#region ENUMS

	#endregion
}
