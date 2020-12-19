using UnityEngine;
using System;

[System.Serializable]
public class PlayerStatisticsController
{
	#region MEMBERS

	private const int DEFAULT_HEALTH_POINTS = 5;
	private const int DEFAULT_SHIELD_POINTS = 2;

	public event Action OnPlayerDead = delegate { };

	[SerializeField]
	private IntValue healthPoints = new IntValue(DEFAULT_HEALTH_POINTS);
	[SerializeField]
	private IntValue shieldsPoints = new IntValue(DEFAULT_SHIELD_POINTS);

	#endregion

	#region PROPERTIES

	public int CurrentHealthPoints => HealthPoints.Value;
	public int CurrentShieldPoints => ShieldsPoints.Value;

	public IntValue HealthPoints {
		get => healthPoints;
		private set => healthPoints = value;
	}

	public IntValue ShieldsPoints {
		get => shieldsPoints;
		private set => shieldsPoints = value;
	}

	#endregion

	#region METHODS

	public void ReloadStatistics()
	{
		HealthPoints.SetValue(DEFAULT_HEALTH_POINTS);
		ShieldsPoints.SetValue(DEFAULT_SHIELD_POINTS);
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
				ShieldsPoints.RemoveValue(1);
			}
			else
			{
				HealthPoints.RemoveValue(1);
			}
		}
	}

    private bool IsShieldActive()
	{
		return ShieldsPoints.Value > 0;
	}

	private bool IsPlayerAlive()
	{
		return HealthPoints.Value > 0;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}