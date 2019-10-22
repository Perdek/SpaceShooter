using UnityEngine;

public class Asteroid : BasePoolObject
{
	#region FIELDS

	[SerializeField]
	private uint speedFactory = 10;

	[SerializeField]
	private Rigidbody2D rigidbody2DComponent = null;

	#endregion

	#region PROPERTIES

	public Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;
	public uint SpeedFactory => speedFactory;

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
		Explosion();
		UpdateManager.Instance.OnUpdatePhysic -= Move;
	}

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	private void Move()
	{
		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + Vector2.down * Time.fixedDeltaTime * SpeedFactory);
	}

	private void Explosion()
	{
		Debug.Log("Explosion");
	}

	private void HandleCollision(Collider2D other)
	{
		if (other.GetComponent<SceneBottonCollider>() != null || other.GetComponent<Bullet>() != null)
		{
			Deactivation();
		}
	}

	private bool CheckCollisionWithPlayerBullet(Collider2D other)
	{
		Bullet bullet = other.GetComponentInChildren<Bullet>();

		return bullet != null ? false : bullet.Iff == IdentificationFriendOrFoeEnum.FRIEND;
	}

	#endregion

	#region ENUMS

	#endregion
}
