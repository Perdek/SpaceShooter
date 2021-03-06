﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AsteroidMovementComponent
{
	#region MEMBERS

	[SerializeField]
	private uint speedFactory = 5;
	[SerializeField]
	private float speedLotteryRangeFactor = 1;
	[SerializeField]
	private Rigidbody2D rigidbody2DComponent = null;
	[SerializeField]
	private float directionLotteryRange = 0.2f;

	#endregion

	#region PROPERTIES

	private Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;
	private uint SpeedFactory => speedFactory;
	private float SpeedLotteryRangeFactor => speedLotteryRangeFactor;
	private float DirectionLotteryRange => directionLotteryRange;

	private float CalculatedSpeedFactor {
		get;
		set;
	} = 1;

	private Vector2 CalculatedDirection {
		get;
		set;
	} = new Vector2();


	#endregion

	#region METHODS

	public void AttachEvents()
	{
		UpdateManager.Instance.OnUpdatePhysic += Move;
	}

	public void DetachEvents()
	{
		UpdateManager.Instance.OnUpdatePhysic -= Move;
	}

	public void Initialize()
	{
		RandomSpeedFactor();
		RandomDirectionValue();
	}

	private void RandomSpeedFactor()
	{
		float minSpeed = SpeedFactory - SpeedLotteryRangeFactor;
		float maxSpeed = SpeedFactory + SpeedLotteryRangeFactor;
		CalculatedSpeedFactor = UnityEngine.Random.Range(minSpeed > 1 ? minSpeed : 1, maxSpeed);
	}

	private void RandomDirectionValue()
	{
		float minDirectionValue = -DirectionLotteryRange;
		float maxDirectionValue = DirectionLotteryRange;
		CalculatedDirection = new Vector2(UnityEngine.Random.Range(minDirectionValue, maxDirectionValue), -1);
	}

	private void Move()
	{
		Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + CalculatedDirection * Time.fixedDeltaTime * CalculatedSpeedFactor);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
