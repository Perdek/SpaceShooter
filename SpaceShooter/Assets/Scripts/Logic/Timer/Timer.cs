using System;
using Managers.GameManagers;
using UnityEngine;

public class Timer
{
	#region FIELDS

	public event Action OnCountingEnd = delegate { };
	    
	private readonly IUpdateManager _updateManager;

	private readonly bool _repeatable;
	private readonly float _targetTime;
	private float _currentTime;
	private float _firstDelay;

	#endregion

	#region PROPERTIES
	
    #endregion

    #region METHODS

	public Timer(IUpdateManager updateManager, float newFirstDelay, float newTargetTime, Action methodToInvokeOnCountingEnd, bool isRepeatable = false)
	{
		_updateManager = updateManager;
		_firstDelay = newFirstDelay;
		_targetTime = newTargetTime;
		OnCountingEnd = methodToInvokeOnCountingEnd;
		_repeatable = isRepeatable;
	}

	public void StartCounting()
	{
		ResetCounter();

		_updateManager.OnDataChange += Counting;
	}

	public void EndCounting()
	{
		_updateManager.OnDataChange -= Counting;
	}

	private void Counting()
	{
		UpdateTimer();
		CheckCountingEnd();
	}

	private void UpdateTimer()
	{
		_currentTime += Time.deltaTime;
	}

	private void ResetCounter()
	{
		_currentTime = 0;
	}

	private void CheckCountingEnd()
	{
		if (_currentTime >= _targetTime)
		{
			if (_repeatable == false)
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
