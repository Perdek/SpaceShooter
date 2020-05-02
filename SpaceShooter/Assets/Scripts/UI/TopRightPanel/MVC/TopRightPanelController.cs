using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopRightPanelController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private TopRightPanelModel Model {
		get;
		set;
	}

	private TopRightPanelView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void RefreshPanel((int hp, int shields) playerStatistics)
	{
		View.RefreshView(playerStatistics.hp, playerStatistics.shields);
	}

	protected virtual void Awake()
	{
		PrepareProperties();
		AttachEvents();
	}

	protected virtual void OnDestroy()
	{
		DetachEvents();
	}

	private void AttachEvents()
	{
		View.AttachEvents();
	}

	private void DetachEvents()
	{
		View.DetachEvents();
	}

	private void PrepareProperties()
	{
		Model = GetModel<TopRightPanelModel>();
		View = GetView<TopRightPanelView>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
