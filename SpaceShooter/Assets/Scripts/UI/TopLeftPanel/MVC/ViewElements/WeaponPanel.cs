using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponPanel
{
	#region MEMBERS

	[SerializeField]
	private Image weaponIconImage = null;
	[SerializeField]
	private Text reloadText = null;

	#endregion

	#region PROPERTIES

	private Text ReloadText => reloadText;
	private Image WeaponIconImage => weaponIconImage;

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
