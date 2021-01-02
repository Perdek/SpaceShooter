using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private IntValueTMP scoreIntValueTMP = null;
	[SerializeField]
	private IntValueTMP moneyIntValueTMP = null;
	[SerializeField]
	private WeaponPanel weaponPanel = null;

	#endregion

	#region PROPERTIES

	private IntValueTMP ScoreIntValueTMP => scoreIntValueTMP;
	private IntValueTMP MoneyIntValueTMP => moneyIntValueTMP;
	private WeaponPanel WeaponPanel => weaponPanel;

	#endregion

	#region FUNCTIONS

	public void RegisterScore(IntValue score)
    {
		ScoreIntValueTMP.RegisterValue(score);
    }

	public void RegisterMoney(IntValue money)
	{
		MoneyIntValueTMP.RegisterValue(money);
	}

	public void RegisterWeapon(WeaponValue weapon)
    {
		WeaponPanel.RegisterWeapon(weapon);
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
