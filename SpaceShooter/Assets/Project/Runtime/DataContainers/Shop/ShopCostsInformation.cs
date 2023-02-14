using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "New Shop Costs Information", menuName = "Shop Costs Information")]
public class ShopCostsInformation : ScriptableObject
{
    #region FIELDS

    [FormerlySerializedAs("hpCost")] [SerializeField] private int _hpCost = 10;
    [FormerlySerializedAs("shieldCost")] [SerializeField] private int _shieldCost = 20;

    #endregion

    #region PROPERTIES

    public int HpCost => _hpCost;
    public int ShieldCost => _shieldCost;

    #endregion

    #region METHODS

    #endregion

    #region ENUMS

    #endregion
}