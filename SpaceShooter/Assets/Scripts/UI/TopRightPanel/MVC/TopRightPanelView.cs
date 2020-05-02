using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private HPPanel hpPanel = null;
	[SerializeField]
	private ShieldsPanel shieldsPanel = null;

	#endregion

	#region PROPERTIES

	private HPPanel HpPanel => hpPanel;
	private ShieldsPanel ShieldsPanel => shieldsPanel;

	#endregion

	#region METHODS

	public void RefreshView(int hpPoints, int shieldPoints)
	{
		HpPanel.RefreshPanel(hpPoints);
		ShieldsPanel.RefreshPanel(shieldPoints);
	}

	public void AttachEvents()
	{
		HpPanel.AttachEvents();
		ShieldsPanel.AttachEvents();
	}

	public void DetachEvents()
	{
		HpPanel.DetachEvents();
		ShieldsPanel.DetachEvents();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
