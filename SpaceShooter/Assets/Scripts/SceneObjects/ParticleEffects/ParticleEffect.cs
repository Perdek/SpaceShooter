using UnityEngine;
using System.Collections;
using System;

public class ParticleEffect : BasePoolObject
{
	#region MEMBERS

	[SerializeField]
	private float lifeTime = 1.0f;
	[SerializeField]
	private ParticleSystem particleSystem = null;

	#endregion

	#region PROPERTIES

	private ParticleSystem ParticleSystem => particleSystem;
	private float LifeTime => lifeTime;

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
		LifeTimer = new Timer(0, LifeTime, Deactivation);
		LifeTimer.StartCounting();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
