using System;
using UnityEngine;

public class Asteroid : BasePoolObject
{
	#region FIELDS

	[SerializeField]
	private AsteroidCollisionComponent collisionComponent;
	[SerializeField]
	private AsteroidMovementComponent movementComponent;
	[SerializeField]
	private AsteroidViewComponent viewComponent;

	#endregion

	#region PROPERTIES

	private AsteroidCollisionComponent CollisionComponent => collisionComponent;
	private AsteroidMovementComponent MovementComponent => movementComponent;
	private AsteroidViewComponent ViewComponent => viewComponent;

	#endregion

	#region METHODS

	public void AttachEvents()
	{
		CollisionComponent.OnHit += Deactivation;

		MovementComponent.AttachEvents();
	}

	public void DetachEvents()
	{
		CollisionComponent.OnHit -= Deactivation;

		MovementComponent.DetachEvents();
	}

	public override void HandleObjectSpawn()
	{
		base.HandleObjectSpawn();

		AttachEvents();
		Initialize();
	}

	public override void Deactivation()
	{
		base.Deactivation();

		Terminate();
		DetachEvents();
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		CollisionComponent.HandleCollision(other);
	}

	private void Initialize()
	{
		MovementComponent.Initialize();
		ViewComponent.SetAsteroidTranform(this.transform);
	}

	private void Terminate()
	{
		ViewComponent.Explosion();
	}

	#endregion

	#region ENUMS

	#endregion
}
