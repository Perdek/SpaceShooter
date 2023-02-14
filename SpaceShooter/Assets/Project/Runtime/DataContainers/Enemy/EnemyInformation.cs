using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Enemy Information", menuName = "Enemy Information")]
public class EnemyInformation : ScriptableObject
{
    #region FIELDS

    [FormerlySerializedAs("scorePointsOnDestroy")] [SerializeField] private int _scorePointsOnDestroy = 1;
    [FormerlySerializedAs("moneyBonusOnDestroy")] [SerializeField] private int _moneyBonusOnDestroy = 1;

    #endregion

    #region PROPERTIES

    public int ScorePointsOnDestroy => _scorePointsOnDestroy;
    public int MoneyBonusOnDestroy => _moneyBonusOnDestroy;

    #endregion

    #region METHODS

    #endregion

    #region ENUMS

    #endregion
}