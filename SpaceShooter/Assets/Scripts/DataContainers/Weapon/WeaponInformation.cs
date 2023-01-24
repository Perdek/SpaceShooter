using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Weapon Information", menuName = "Weapon Information")]
public class WeaponInformation : ScriptableObject
{
    #region FIELDS

    [FormerlySerializedAs("magazineCapacityCurve")] [SerializeField] private AnimationCurve _magazineCapacityCurve;
    [FormerlySerializedAs("timeInSecondBetweenShootsCurve")] [SerializeField] private AnimationCurve _timeInSecondBetweenShootsCurve;
    [FormerlySerializedAs("reloadingTimeInSecondsCurve")] [SerializeField] private AnimationCurve _reloadingTimeInSecondsCurve;
    [FormerlySerializedAs("damageCurve")] [SerializeField] private AnimationCurve _damageCurve;
    [FormerlySerializedAs("upgradingCostCurve")] [SerializeField] private AnimationCurve _upgradingCostCurve;
    [FormerlySerializedAs("weaponName")] [SerializeField] private string _weaponName = "weapon";
    [FormerlySerializedAs("bulletSprite")] [SerializeField] private Sprite _bulletSprite = null;

    #endregion

    #region PROPERTIES

    public AnimationCurve MagazineCapacityCurve => _magazineCapacityCurve;
    public AnimationCurve TimeInSecondBetweenShootsCurve => _timeInSecondBetweenShootsCurve;
    public AnimationCurve ReloadingTimeInSecondsCurve => _reloadingTimeInSecondsCurve;
    public AnimationCurve DamageCurve => _damageCurve;
    public AnimationCurve UpgradingCostCurve => _upgradingCostCurve;
    public string WeaponName => _weaponName;
    public Sprite BulletSprite => _bulletSprite;

    #endregion

    #region METHODS

    #endregion

    #region ENUMS

    #endregion
}