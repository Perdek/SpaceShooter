using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Information", menuName = "Weapon Information")]
public class WeaponInformation : ScriptableObject
{
    #region FIELDS

    [SerializeField]
    private AnimationCurve magazineCapacityCurve;
    [SerializeField]
    private AnimationCurve timeInSecondBetweenShootsCurve;
    [SerializeField]
    private AnimationCurve reloadingTimeInSecondsCurve;
    [SerializeField]
    private AnimationCurve damageCurve;
    [SerializeField]
    private AnimationCurve upgradingCostCurve;
    [SerializeField]
    private string weaponName = "weapon";
    [SerializeField]
    private Sprite bulletSprite = null;

    #endregion

    #region PROPERTIES

	public AnimationCurve MagazineCapacityCurve => magazineCapacityCurve;
	public AnimationCurve TimeInSecondBetweenShootsCurve => timeInSecondBetweenShootsCurve;
	public AnimationCurve ReloadingTimeInSecondsCurve => reloadingTimeInSecondsCurve;
	public AnimationCurve DamageCurve => damageCurve;
    public AnimationCurve UpgradingCostCurve => upgradingCostCurve;
    public string WeaponName => weaponName;
    public Sprite BulletSprite => bulletSprite;


    #endregion

    #region METHODS

    #endregion

    #region ENUMS

    #endregion
}
