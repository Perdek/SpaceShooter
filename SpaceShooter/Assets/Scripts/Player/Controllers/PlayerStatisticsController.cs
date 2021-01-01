using UnityEngine;
using System;

[System.Serializable]
public class PlayerStatisticsController
{
	#region MEMBERS

	public event Action OnPlayerDead = delegate { };

	[SerializeField, Range(0,10)] private int defaultHealthPoints = 5;
	[SerializeField, Range(0,10)] private int defaultShieldPoints = 2;
	[SerializeField, Range(0,100)] private int defaultScorePoints = 5;
	[SerializeField, Range(0,100)] private int defaultMoneyPoints = 2;

	[SerializeField] private IntValue healthPoints = new IntValue();
	[SerializeField] private IntValue shieldsPoints = new IntValue();
	[SerializeField] private IntValue scorePoints = new IntValue();
	[SerializeField] private IntValue moneyPoints = new IntValue();

	#endregion

	#region PROPERTIES

	public int CurrentHealthPoints => HealthPoints.Value;
	public int CurrentShieldPoints => ShieldsPoints.Value;
	public int CurrentScorePoints => ScorePoints.Value;
	public int CurrentMoneyPoints => MoneyPoints.Value;

	public IntValue HealthPoints {
		get => healthPoints;
		private set => healthPoints = value;
	}

	public IntValue ShieldsPoints {
		get => shieldsPoints;
		private set => shieldsPoints = value;
	}

	public IntValue ScorePoints {
		get => scorePoints;
		private set => scorePoints = value;
	}

	public IntValue MoneyPoints {
		get => moneyPoints;
		private set => moneyPoints = value;
	}

	#endregion

	#region METHODS

	public void ReloadStatistics()
	{
		HealthPoints.SetValue(defaultHealthPoints);
		ShieldsPoints.SetValue(defaultShieldPoints);
        ScorePoints.SetValue(defaultScorePoints);
        MoneyPoints.SetValue(defaultMoneyPoints);
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

	public void RewardForKill(EnemyInformation killedEnemyInformation)
	{
		MoneyPoints.AddValue(killedEnemyInformation.MoneyBonusOnDestroy);
		ScorePoints.AddValue(killedEnemyInformation.ScorePointsOnDestroy);
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