﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMovementController
{
	#region FIELDS

	private const float MIN_SPEED_TO_SET = 0;
	private const float MAX_SPEED_TO_SET = 100;

	[Header("Movement")]
	[SerializeField, Range(MIN_SPEED_TO_SET, MAX_SPEED_TO_SET)]
	private float maxSpeed = 1;
	[SerializeField]
	private float accelerationFactory = 1;
	[SerializeField]
	private float brakingFactory = 1;
	[SerializeField]
	private Rigidbody2D playerRigidBody2D = null;

	private MovingStateEnum state = MovingStateEnum.IDLE;

	#endregion

	#region PROPERTIES

	private Rigidbody2D PlayerRigidBody2D => playerRigidBody2D;

	private float AccelerationFactory {
		get => accelerationFactory;
		set => accelerationFactory = value;
	}

	private float BrakingFactory {
		get => brakingFactory;
		set => brakingFactory = value;
	}

    private float MaxSpeed {
		get => maxSpeed;
		set => maxSpeed = value;
	}

	private MovingStateEnum State {
		get => state;
		set => state = value;
	}

	private List<int> KeysIds {
		get;
		set;
	} = new List<int>();

	#endregion

	#region METHODS

	public void Initialize()
	{
		InitializeKeys();
		InitializeUpdate();
	}

	public void ResetPosition()
	{
		PlayerRigidBody2D.transform.position = Vector3.zero;
	}

	public void MoveUp()
	{
		PlayerRigidBody2D.AddForce(Vector2.up * AccelerationFactory);
	}

	public void MoveDown()
	{
		PlayerRigidBody2D.AddForce(Vector2.down * AccelerationFactory);
	}

	public void MoveRight()
	{
		PlayerRigidBody2D.AddForce(Vector2.right * AccelerationFactory);
	}

	public void MoveLeft()
	{
		PlayerRigidBody2D.AddForce(Vector2.left * AccelerationFactory);
	}

	public void MoveUpRight()
	{
		PlayerRigidBody2D.AddForce(new Vector2(1, 1) * AccelerationFactory);
	}

	public void MoveUpLeft()
	{
		PlayerRigidBody2D.AddForce(new Vector2(-1, 1) * AccelerationFactory);
	}

	public void MoveDownLeft()
	{
		PlayerRigidBody2D.AddForce(new Vector2(-1, -1) * AccelerationFactory);
	}

	public void MoveDownRight()
	{
		PlayerRigidBody2D.AddForce(new Vector2(1, -1) * AccelerationFactory);
	}

	public void Brake()
	{
		PlayerRigidBody2D.AddForce(-BrakingFactory * PlayerRigidBody2D.velocity);
	}

	public void LimitVelocity()
	{
		Vector3 normalizedVelocity = PlayerRigidBody2D.velocity.normalized;
		Vector3 brakeVelocity = normalizedVelocity * MaxSpeed;  // make the brake Vector3 value

		PlayerRigidBody2D.velocity = brakeVelocity;  // apply opposing brake force
	}

	private void HandleVelocityLimit()
	{
		if (State != MovingStateEnum.BREAKING && PlayerRigidBody2D.velocity.magnitude > MaxSpeed)
		{
			LimitVelocity();
		}
	}

	private void InitializeKeys()
	{
		GameMainManager.Instance.OnGameStart += AttachKeysForMovement;
		GameMainManager.Instance.OnWaitingOpen += DetachKeysForMovement;
		GameMainManager.Instance.OnMainOpen += DetachKeysForMovement;
	}

	private void AttachKeysForMovement()
	{
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeyCodeMoveUp(), SetMoveUp));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeyCodeMoveDown(), SetMoveDown));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeyCodeMoveLeft(), SetMoveLeft));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeyCodeMoveRight(), SetMoveRight));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeysCodeMoveUpRight(), SetMoveUpRight));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeysCodeMoveUpLeft(), SetMoveUpLeft));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeysCodeMoveDownRight(), SetMoveDownRight));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeysCodeMoveDownLeft(), SetMoveDownLeft));
		KeysIds.Add(KeyboardManager.Instance.AddKey(GetKeysCodeMovement(), SetBreak, KeyInput.KeyStateEnum.KEY_HOLD, KeyInput.CheckingModeEnum.CONJUNCTION, KeyInput.OccurrenceModeEnum.KEY_HAS_NOT_OCCUR));
	}

	private void DetachKeysForMovement()
	{
		for (int i = KeysIds.Count - 1; i >= 0; i--)
		{
			KeyboardManager.Instance.RemoveKey(KeysIds[i]);
			KeysIds.RemoveAt(i);
		}
	}

	private void InitializeUpdate()
	{
		UpdateManager.Instance.OnDataChange += HandleState;
	}

	private void SetState(MovingStateEnum newState)
	{
		State = newState;
	}

	private void SetMoveUp()
	{
		SetState(MovingStateEnum.MOVING_UP);
	}

	private void SetMoveDown()
	{
		SetState(MovingStateEnum.MOVING_DOWN);
	}
	private void SetMoveLeft()
	{
		SetState(MovingStateEnum.MOVING_LEFT);
	}

	private void SetMoveRight()
	{
		SetState(MovingStateEnum.MOVING_RIGHT);
	}

	private void SetMoveUpLeft()
	{
		SetState(MovingStateEnum.MOVING_UP_LEFT);
	}

	private void SetMoveUpRight()
	{
		SetState(MovingStateEnum.MOVING_UP_RIGHT);
	}

	private void SetMoveDownLeft()
	{
		SetState(MovingStateEnum.MOVING_DOWN_LEFT);
	}

	private void SetMoveDownRight()
	{
		SetState(MovingStateEnum.MOVING_DOWN_RIGHT);
	}

	private void SetBreak()
	{
		SetState(MovingStateEnum.BREAKING);
	}

	private void HandleState()
	{
		switch (State)
		{
			case MovingStateEnum.MOVING_DOWN:
				MoveDown();
				break;
			case MovingStateEnum.MOVING_UP:
				MoveUp();
				break;
			case MovingStateEnum.MOVING_RIGHT:
				MoveRight();
				break;
			case MovingStateEnum.MOVING_LEFT:
				MoveLeft();
				break;
			case MovingStateEnum.MOVING_UP_LEFT:
				MoveUpLeft();
				break;
			case MovingStateEnum.MOVING_UP_RIGHT:
				MoveUpRight();
				break;
			case MovingStateEnum.MOVING_DOWN_RIGHT:
				MoveDownRight();
				break;
			case MovingStateEnum.MOVING_DOWN_LEFT:
				MoveDownLeft();
				break;
			case MovingStateEnum.BREAKING:
				Brake();
				break;
		}

		HandleVelocityLimit();
	}

	private KeyCode GetKeyCodeMoveUp()
	{
		return InputManager.Instance.KeyCodeMoveUp;
	}

	private KeyCode GetKeyCodeMoveDown()
	{
		return InputManager.Instance.KeyCodeMoveDown;
	}

	private KeyCode GetKeyCodeMoveLeft()
	{
		return InputManager.Instance.KeyCodeMoveLeft;
	}

	private KeyCode GetKeyCodeMoveRight()
	{
		return InputManager.Instance.KeyCodeMoveRight;
	}

	private List<KeyCode> GetKeysCodeMoveUpRight()
	{
		return new List<KeyCode>() { GetKeyCodeMoveUp(), GetKeyCodeMoveRight() };
	}

	private List<KeyCode> GetKeysCodeMoveUpLeft()
	{
		return new List<KeyCode>() { GetKeyCodeMoveUp(), GetKeyCodeMoveLeft() };
	}

	private List<KeyCode> GetKeysCodeMoveDownRight()
	{
		return new List<KeyCode>() { GetKeyCodeMoveDown(), GetKeyCodeMoveRight() };
	}

	private List<KeyCode> GetKeysCodeMoveDownLeft()
	{
		return new List<KeyCode>() { GetKeyCodeMoveDown(), GetKeyCodeMoveLeft() };
	}

	private List<KeyCode> GetKeysCodeMovement()
	{
		return new List<KeyCode>() { GetKeyCodeMoveDown(), GetKeyCodeMoveUp(), GetKeyCodeMoveLeft(), GetKeyCodeMoveRight() };
	}

	#endregion

	#region ENUMS

	public enum MovingStateEnum
	{
		IDLE,
		BREAKING,
		MOVING_UP,
		MOVING_UP_LEFT,
		MOVING_UP_RIGHT,
		MOVING_LEFT,
		MOVING_RIGHT,
		MOVING_DOWN,
		MOVING_DOWN_LEFT,
		MOVING_DOWN_RIGHT,
	}

	#endregion
}
