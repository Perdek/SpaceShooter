using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BasePoolObject
{
	#region FIELDS

	[SerializeField]
	private Rigidbody2D rigidbody2DComponent = null;

	#endregion

	#region PROPERTIES

	public Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;

	#endregion

	#region METHODS

	public override void HandleObjectSpawn()
	{
		if (State == PoolObjectState.WAITING_FOR_USE)
		{
			UpdateManager.Instance.OnUpdate += Move;
		}
	}

	public override void Deactivation()
	{
		base.Deactivation();
		UpdateManager.Instance.OnUpdate -= Move;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	private void Move()
	{
		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + Vector2.up * Time.fixedDeltaTime);
	}

	private void HandleCollision(Collider2D other)
	{
		if (CheckCollisionWithPlayer(other) == true)
		{
			return;
		}

		base.Deactivation();
	}

	private bool CheckCollisionWithPlayer(Collider2D other)
	{
		return other.GetComponent<PlayerColliderController>() != null;
	}

	#endregion

	#region ENUMS

	#endregion
}
