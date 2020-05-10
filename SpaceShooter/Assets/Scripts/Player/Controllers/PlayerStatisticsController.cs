using UnityEngine;
using System;

[System.Serializable]
public class PlayerStatisticsController
{
	#region MEMBERS

	public event Action OnPlayerDead = delegate { };
	public event Action OnHealthPointsAdd = delegate { };
	public event Action OnHealthPointsRemove = delegate { };
	public event Action OnShieldsPointsAdd = delegate { };
	public event Action OnShieldsPointsRemove = delegate { };
	public event Action<int> OnHealthPointsChange = delegate { };
	public event Action<int> OnShieldsPointsChange = delegate { };

	[SerializeField]
	private int healthPoints = 5;
	[SerializeField]
	private int shieldsPoints = 2;

	#endregion

	#region PROPERTIES

	public int HealthPoints {
		get => healthPoints;
		private set => healthPoints = value;
	}

	public int ShieldsPoints {
		get => shieldsPoints;
		private set => shieldsPoints = value;
	}

	#endregion

	#region METHODS

	public void AddHealthPoints(int value)
	{
		HealthPoints += value;
		OnHealthPointsAdd();
		OnHealthPointsChange(HealthPoints);
	}

	public void RemoveHealthPoints(int value = 1)
	{
		HealthPoints -= value;
		OnHealthPointsRemove();
		OnHealthPointsChange(HealthPoints);
	}

	public void AddShieldsPoints(int value)
	{
		ShieldsPoints += value;
		OnShieldsPointsAdd();
		OnShieldsPointsChange(value);
	}

	public void RemoveShieldsPoints(int value = 1)
	{
		ShieldsPoints -= value;
		OnShieldsPointsRemove();
		OnShieldsPointsChange(value);
	}

	public void HandleDamage(int damage)
	{
		for (int i = 0; i < damage; i++)
		{
			if (IsPlayerAlive() == false)
			{
				OnPlayerDead();
				return;
			}

			if (IsShieldActive() == true)
			{
				RemoveShieldsPoints();
			}
			else
			{
				RemoveHealthPoints();
			}
		}
	}

	private bool IsShieldActive()
	{
		return ShieldsPoints > 0;
	}

	private bool IsPlayerAlive()
	{
		return HealthPoints > 0;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}