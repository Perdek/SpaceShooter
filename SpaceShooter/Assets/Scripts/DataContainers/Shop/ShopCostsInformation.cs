using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Costs Information", menuName = "Shop Costs Information")]
public class ShopCostsInformation : ScriptableObject
{
	#region FIELDS

	[SerializeField]
	private int hpCost = 10;
	[SerializeField]
	private int shieldCost = 20;

	#endregion

	#region PROPERTIES

	public int HpCost => hpCost;
	public int ShieldCost => shieldCost;

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}
