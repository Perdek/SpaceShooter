using UnityEngine;

public class Bullet : BasePoolObject
{
	#region FIELDS

	[SerializeField]
	private uint speedFactory = 10;

	[SerializeField]
	private Rigidbody2D rigidbody2DComponent = null;

	[SerializeField]
	private IdentificationFriendOrFoeEnum iff = IdentificationFriendOrFoeEnum.FOE;

	#endregion

	#region PROPERTIES

	public Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;
	public uint SpeedFactory => speedFactory;
	public IdentificationFriendOrFoeEnum Iff => iff;

	#endregion

	#region METHODS

	public override void HandleObjectSpawn()
	{
		if (State == PoolObjectStateEnum.WAITING_FOR_USE)
		{
			UpdateManager.Instance.OnUpdatePhysic += Move;
		}
	}

	public override void Deactivation()
	{
		base.Deactivation();
		UpdateManager.Instance.OnUpdatePhysic -= Move;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	private void Move()
	{
		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + Vector2.up * Time.fixedDeltaTime * SpeedFactory);
	}

	private void HandleCollision(Collider2D other)
	{
		if (CheckCollisionWithPlayer(other) == true)
		{
			return;
		}

		Deactivation();
	}

	private bool CheckCollisionWithPlayer(Collider2D other)
	{
		return other.GetComponentInChildren<PlayerColliderController>() != null;
	}

	#endregion

	#region ENUMS

	#endregion
}
