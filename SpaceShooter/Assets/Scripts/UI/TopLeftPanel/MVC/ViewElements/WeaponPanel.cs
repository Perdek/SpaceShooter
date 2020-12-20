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
	private TMPro.TMP_Text reloadText = null;

	#endregion

	#region PROPERTIES

	private TMPro.TMP_Text ReloadText => reloadText;
	private Image WeaponIconImage => weaponIconImage;

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
