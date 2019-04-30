using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingController : MonoBehaviour
{
	#region FIELDS

	[SerializeField]
	private Transform playerBulletSpawnTransform = null;

	#endregion

	#region PROPERTIES

	public Transform PlayerBulletSpawnTransform => playerBulletSpawnTransform;

	#endregion

	#region METHODS

	public void Initialize()
	{
		InitializeKeys();
	}

	public void Shoot()
	{
		if (PoolManager.Instance != null)
		{
			Debug.Log("Shoot");
			PoolManager.Instance.GetPoolObject(TagManager.PLAYER_BULLET_TAG, PlayerBulletSpawnTransform.position, PlayerBulletSpawnTransform.rotation);
		}
	}

	private void InitializeKeys()
	{
		KeyboardManager.Instance.AddKey(KeyCode.Space, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN);
	}

	#endregion

	#region ENUMS

	#endregion
}
