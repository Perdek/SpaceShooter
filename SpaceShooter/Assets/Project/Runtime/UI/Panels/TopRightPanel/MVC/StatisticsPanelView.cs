using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private IntValueVisualizationPanel hpPanel = null;
	[SerializeField]
	private IntValueVisualizationPanel shieldsPanel = null;

	#endregion

	#region PROPERTIES

	private IntValueVisualizationPanel HpPanel => hpPanel;
	private IntValueVisualizationPanel ShieldsPanel => shieldsPanel;

	#endregion

	#region METHODS

	public void RefreshView(int hpPoints, int shieldPoints)
	{
		HpPanel.RefreshPanel(hpPoints);
		ShieldsPanel.RefreshPanel(shieldPoints);
	}

	public void RegisterShieldPoints(IntValue shieldsPoints)
	{
		ShieldsPanel.RegisterValue(shieldsPoints);
	}

	public void RegisterHealthPoints(IntValue healthPoints)
	{
		HpPanel.RegisterValue(healthPoints);
	}

	public void UnregisterHealthPoints()
	{
		ShieldsPanel.UnregisterValue();
	}

	public void UnregisterShieldPoints()
	{
		HpPanel.UnregisterValue();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
