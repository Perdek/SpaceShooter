using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
	#region FIELDS

	public System.Action OnCountingEnd = delegate { };
	private float firstSpawnDelayInSeconds;
	private float delayBetweenSpawnsInSeconds;
	private Action spawn;

	#endregion

	#region PROPERTIES

	private bool Repeatable {
		get;
		set;
	} = false;

	private float CurrentTime {
		get;
		set;
	} = 0;

	private float TargetTime {
		get;
		set;
	} = 1f;

	private float FirstDelay {
		get;
		set;
	} = 0f;

	#endregion

	#region METHODS

	public Timer(float firstSpawnDelayInSeconds, float delayBetweenSpawnsInSeconds, Action spawn)
	{
		this.firstSpawnDelayInSeconds = firstSpawnDelayInSeconds;
		this.delayBetweenSpawnsInSeconds = delayBetweenSpawnsInSeconds;
		this.spawn = spawn;
	}

	public void StartCounting()
	{
		UpdateManager.Instance.OnDataChange += Counting;
	}

	public void EndCouting()
	{
		UpdateManager.Instance.OnDataChange -= Counting;
	}

	private void Counting()
	{
		UpdateTimer();
		CheckCountingEnd();
	}

	private void UpdateTimer()
	{
		CurrentTime += Time.deltaTime;
	}

	private void ResetCounter()
	{
		CurrentTime = 0;
	}

	private void CheckCountingEnd()
	{
		if (CurrentTime >= TargetTime)
		{
			if (Repeatable == false)
			{
				EndCouting();
			}
			else
			{
				ResetCounter();
			}

			OnCountingEnd();
		}
	}

	#endregion

	#region ENUMS

	#endregion
}
