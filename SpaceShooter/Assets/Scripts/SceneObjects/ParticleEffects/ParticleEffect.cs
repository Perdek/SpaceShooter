using UnityEngine;
using System.Collections;
using System;

public class ParticleEffect : BasePoolObject
{
	#region MEMBERS

	[SerializeField]
	private ParticleSystem particleSystem = null;

	#endregion

	#region PROPERTIES

	private ParticleSystem ParticleSystem => particleSystem;

	private Timer LifeTimer {
		get;
		set;
	} = null;

	#endregion

	#region FUNCTIONS

	protected virtual void Awake()
	{
		AttachEvents();
	}

	protected virtual void OnDestroy()
	{
		DetachEvents();
	}

	private void AttachEvents()
	{
		OnHandleObjectSpawn += HandleLifeTime;
	}

	private void DetachEvents()
	{
		OnHandleObjectSpawn -= HandleLifeTime;
	}

	private void HandleLifeTime()
	{
		LifeTimer = new Timer(0, ParticleSystem.time, Deactivation);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
