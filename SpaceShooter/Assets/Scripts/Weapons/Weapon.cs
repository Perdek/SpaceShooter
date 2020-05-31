using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
	#region FIELDS

	[SerializeField]
	private WeaponInformation weaponInformation = null;

	#endregion

	#region PROPERTIES

	private WeaponInformation WeaponInformation => weaponInformation;

	#endregion

	#region METHODS

	#endregion

	#region ENUMS

	#endregion
}
