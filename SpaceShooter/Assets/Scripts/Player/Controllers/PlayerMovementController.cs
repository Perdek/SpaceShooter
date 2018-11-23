using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
	#region FIELDS

	[SerializeField]
	private Rigidbody2D playerRigidBody2D = null;

	#endregion

	#region PROPERTIES

	public Rigidbody2D PlayerRigidBody2D => playerRigidBody2D;

	public float Speed {
		get;
		private set;
	}

	#endregion

	#region METHODS

	public void Initialize(float newSpeed)
	{
		InitializeKeys();
		SetSpeed(newSpeed);
	}

	public void SetSpeed(float newSpeed)
	{
		Speed = newSpeed;
	}

	public void MoveUp()
	{
		PlayerRigidBody2D.AddForce(Vector2.up * Speed);
	}

	public void MoveDown()
	{
		PlayerRigidBody2D.AddForce(Vector2.down * Speed);
	}

	public void MoveRight()
	{
		PlayerRigidBody2D.AddForce(Vector2.right * Speed);
	}

	public void MoveLeft()
	{
		PlayerRigidBody2D.AddForce(Vector2.left * Speed);
	}

	private void InitializeKeys()
	{
		KeyboardManager.Instance.AddKey(KeyCode.W, KeyInput.InputMode.KEY_HOLD, MoveUp);
		KeyboardManager.Instance.AddKey(KeyCode.S, KeyInput.InputMode.KEY_HOLD, MoveDown);
		KeyboardManager.Instance.AddKey(KeyCode.A, KeyInput.InputMode.KEY_HOLD, MoveLeft);
		KeyboardManager.Instance.AddKey(KeyCode.D, KeyInput.InputMode.KEY_HOLD, MoveRight);
	}

	#endregion

	#region ENUMS

	#endregion
}
