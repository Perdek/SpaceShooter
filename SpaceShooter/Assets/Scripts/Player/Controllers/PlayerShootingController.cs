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

	private List<int> KeysIds {
		get;
		set;
	} = new List<int>();

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
		GameMainManager.Instance.OnGameStart += AttachKeysForShooting;
		GameMainManager.Instance.OnMainOpen += DetachKeysForShooting;
	}

	private void AttachKeysForShooting()
	{
		KeysIds.Add(KeyboardManager.Instance.AddKey(KeyCode.Space, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN));
	}

	private void DetachKeysForShooting()
	{
		for (int i = KeysIds.Count - 1; i >= 0 ; i--)
		{
			KeyboardManager.Instance.RemoveKey(KeysIds[i]);
			KeysIds.RemoveAt(i);
		}
	}

	#endregion

	#region ENUMS

	#endregion
}
