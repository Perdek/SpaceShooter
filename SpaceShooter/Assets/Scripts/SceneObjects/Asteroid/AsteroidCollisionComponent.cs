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

	#endregion

	#region METHODS

	public void HandleCollision(Collider2D other)
	{
		if (other.GetComponent<SceneBottonCollider>() != null || other.GetComponent<Bullet>() != null)
		{
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
