using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlayerStatisticsController
{
    #region MEMBERS

    public event Action OnPlayerDead = delegate { };
    public event Action OnTurnOnForceShield = delegate { };
    public event Action OnTurnOffForceShield = delegate { };

    [FormerlySerializedAs("defaultHealthPoints")] [SerializeField, Range(0, 10)] private int _defaultHealthPoints = 5;
    [FormerlySerializedAs("defaultShieldPoints")] [SerializeField, Range(0, 10)] private int _defaultShieldPoints = 2;
    [FormerlySerializedAs("defaultScorePoints")] [SerializeField, Range(0, 100)] private int _defaultScorePoints = 5;
    [FormerlySerializedAs("defaultMoneyPoints")] [SerializeField, Range(0, 100)] private int _defaultMoneyPoints = 2;

    [SerializeField] private IntValue _healthPoints = new IntValue();
    [SerializeField] private IntValue _shieldsPoints = new IntValue();
    [SerializeField] private IntValue _scorePoints = new IntValue();
    [SerializeField] private IntValue _moneyPoints = new IntValue();

    #endregion

    #region PROPERTIES

    public int CurrentHealthPoints => HealthPoints.Value;
    public int CurrentShieldPoints => ShieldsPoints.Value;
    public int CurrentScorePoints => ScorePoints.Value;
    public int CurrentMoneyPoints => MoneyPoints.Value;

    public IntValue HealthPoints => _healthPoints;
    public IntValue ShieldsPoints => _shieldsPoints;
    public IntValue ScorePoints => _scorePoints;
    public IntValue MoneyPoints => _moneyPoints;
    
    #endregion

    #region METHODS

    public void ReloadStatistics()
    {
        HealthPoints.SetValue(_defaultHealthPoints);
        ShieldsPoints.SetValue(_defaultShieldPoints);
        ScorePoints.SetValue(_defaultScorePoints);
        MoneyPoints.SetValue(_defaultMoneyPoints);
    }

    public void AddNewShield(int value)
    {
        if (IsShieldActive() == false)
        {
            OnTurnOnForceShield();
        }

        ShieldsPoints.AddValue(value);
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

                if (IsShieldActive() == false)
                {
                    OnTurnOffForceShield();
                }
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