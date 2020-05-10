using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private ScorePanel scorePanel = null;
	[SerializeField]
	private MoneyPanel moneyPanel = null;
	[SerializeField]
	private WeaponPanel weaponPanel = null;

	#endregion

	#region PROPERTIES

	private ScorePanel ScorePanel => scorePanel;
	private MoneyPanel MoneyPanel => moneyPanel;
	private WeaponPanel WeaponPanel => weaponPanel;

	#endregion

	#region FUNCTIONS

	#endregion

	#region CLASS_ENUMS

	#endregion
}
