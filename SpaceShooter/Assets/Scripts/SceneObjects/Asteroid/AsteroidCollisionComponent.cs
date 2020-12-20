using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidCollisionComponent
{
	#region MEMBERS

	public event Action OnHit = delegate { };	

	#endregion

	#region PROPERTIES

	private int DamageCollisionValue {
		get;
		set;
	} = 1;

	private EnemyInformation CachedEnemyInformation {
		get;
		set;
    }

	#endregion

	#region METHODS

	public void SetDamageCollisionValue(int newValue)
	{
		DamageCollisionValue = newValue;
	}

	public void SetAsteroidInformation(EnemyInformation cachedEnemyInformation)
    {
		CachedEnemyInformation = cachedEnemyInformation;
	}

	public void HandleCollision(Collider2D other)
	{
		Bullet bullet = other.GetComponent<Bullet>();

		if (bullet != null)
        {
			bullet.NotifyComfirmKill(CachedEnemyInformation);
			OnHit();
			return;
        }

		if (other.GetComponent<SceneBottonCollider>() != null)
		{
			OnHit();
		}

		PlayerColliderController playerCollider = other.GetComponentInChildren<PlayerColliderController>();

		if (playerCollider != null)
		{
			playerCollider.DamageCollision(DamageCollisionValue);
			OnHit();
		}
	}

	private bool CheckCollisionWithPlayerBullet(Collider2D other)
	{
		Bullet bullet = other.GetComponentInChildren<Bullet>();

		return bullet != null ? false : bullet.Iff == IdentificationFriendOrFoeEnum.FRIEND;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
