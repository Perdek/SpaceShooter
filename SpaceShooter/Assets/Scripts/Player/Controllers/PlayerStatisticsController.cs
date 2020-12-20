using UnityEngine;
using System;

[System.Serializable]
public class PlayerStatisticsController
{
    #region MEMBERS

    private const int DEFAULT_HEALTH_POINTS = 5;
    private const int DEFAULT_SHIELD_POINTS = 2;
    private const int DEFAULT_SCORE_POINTS = 5;
    private const int DEFAULT_MONEY_POINTS = 2;

    public event Action OnPlayerDead = delegate { };

    [SerializeField]
    private IntValue healthPoints = new IntValue(DEFAULT_HEALTH_POINTS);
    [SerializeField]
    private IntValue shieldsPoints = new IntValue(DEFAULT_SHIELD_POINTS);
    [SerializeField]
    private IntValue scorePoints = new IntValue(DEFAULT_SCORE_POINTS);
    [SerializeField]
    private IntValue moneyPoints = new IntValue(DEFAULT_MONEY_POINTS);

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