using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : BaseMonoBehaviourSingletonManager<UpdateManager>
{
	#region FIELDS

	public event System.Action OnUpdate = delegate { };

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	protected virtual void FixedUpdate()
	{
		HandleOnUpdate();
	}

	private void HandleOnUpdate()
	{
		OnUpdate();
	}

	#endregion

	#region ENUMS

	#endregion
}