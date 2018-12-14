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

	public float SpeedToAdd {
		get;
		private set;
	}

	public float MaxSpeed {
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

	public void SetSpeed(float newSpeedToAdd)
	{
		SpeedToAdd = newSpeedToAdd;
	}

	public void SetMaxSpeed(float newMaxSpeed)
	{
		MaxSpeed = newMaxSpeed;
	}

	public void MoveUp()
	{
		PlayerRigidBody2D.AddForce(Vector2.up * SpeedToAdd);
	}

	public void MoveDown()
	{
		PlayerRigidBody2D.AddForce(Vector2.down * SpeedToAdd);
	}

	public void MoveRight()
	{
		PlayerRigidBody2D.AddForce(Vector2.right * SpeedToAdd);
	}

	public void MoveLeft()
	{
		PlayerRigidBody2D.AddForce(Vector2.left * SpeedToAdd);
	}

	public void Brake()
	{
		//PlayerRigidBody2D
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
