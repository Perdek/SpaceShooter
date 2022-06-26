using Managers.GameManagers;
using System;
using UnityEngine;
using Zenject;

public class Timer
{
	#region FIELDS

	public event Action OnCountingEnd = delegate { };
	    
	private IUpdateManager updateManager;

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

	public Timer(IUpdateManager updateManager, float newFirstDelay, float newTargetTime, Action methodToInvokeOnCountingEnd, bool isRepeatable = false)
	{
		this.updateManager = updateManager;
		FirstDelay = newFirstDelay;
		TargetTime = newTargetTime;
		OnCountingEnd = methodToInvokeOnCountingEnd;
		Repeatable = isRepeatable;
	}

	public void StartCounting()
	{
		ResetCounter();

		updateManager.OnDataChange += Counting;
	}

	public void EndCounting()
	{
		updateManager.OnDataChange -= Counting;
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
				EndCounting();
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
