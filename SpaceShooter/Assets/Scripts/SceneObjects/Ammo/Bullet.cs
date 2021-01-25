using UnityEngine;
using System;

public class Bullet : BasePoolObject
{
	#region FIELDS

	public event Action<EnemyInformation> OnKillTarget = delegate { };

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
		OnKillTarget = null;
		UpdateManager.Instance.OnUpdatePhysic -= Move;
	}

	public void NotifyComfirmKill(EnemyInformation enemyInformation)
    {
		OnKillTarget(enemyInformation);
    }

	protected virtual void OnTriggerEnter2D(Collider2D other)
	{
		HandleCollision(other);
	}

	public void Move()
	{
		Vector2 localForwardVector = new Vector2(transform.up.x, transform.up.y);

		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + localForwardVector * Time.fixedDeltaTime * SpeedFactory);

		Debug.Log(localForwardVector);
	}

	private void HandleCollision(Collider2D hittedObjectCollider)
	{
		if (CheckCollisionWithPlayer(hittedObjectCollider) == true)
		{
			return;
		}

		Enemy enemy = hittedObjectCollider.GetComponentInChildren<Enemy>();

		if (enemy != null)
        {
			NotifyComfirmKill(enemy.EnemyInformation);
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
