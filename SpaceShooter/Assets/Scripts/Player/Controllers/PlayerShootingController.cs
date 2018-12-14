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
		Debug.Log("Shoot");
	}

	private void InitializeKeys()
	{
		KeyboardManager.Instance.AddKey(KeyCode.Space, KeyInput.InputMode.KEY_PRESSED_DOWN, Shoot);
	}

	#endregion

	#region ENUMS

	#endregion
}
