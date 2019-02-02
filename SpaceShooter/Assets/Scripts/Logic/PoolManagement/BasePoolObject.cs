using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePoolObject : MonoBehaviour
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	public PoolObjectState State {
		get;
		private set;
	}

	#endregion

	#region METHODS

	public void SetState(PoolObjectState newState)
	{
		State = newState;
	}

	public virtual void HandleObjectSpawn()
	{

	}

	public virtual void Deactivation()
	{
		SetState(PoolObjectState.WAITING_FOR_USE);
		gameObject.SetActive(false);
	}

	#endregion

	#region ENUMS

	public enum PoolObjectState
	{
		WAITING_FOR_USE,
		IN_USE
	}

	

	#endregion
}
