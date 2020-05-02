using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShootingController
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
			PoolManager.Instance.GetPoolObject(TagManager.TagsEnum.PLAYER_BULLET_TAG, PlayerBulletSpawnTransform.position, PlayerBulletSpawnTransform.rotation);
		}
	}

	private void InitializeKeys()
	{
		GameMainManager.Instance.OnGameStart += AttachKeysForShooting;
		GameMainManager.Instance.OnMainOpen += DetachKeysForShooting;
	}

	private void AttachKeysForShooting()
	{
		KeysIds.Add(KeyboardManager.Instance.AddKey(KeyCode.Space, Shoot, KeyInput.KeyStateEnum.KEY_PRESSED_DOWN, KeyInput.CheckingModeEnum.DISJUNCTION));
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
