using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsPanelController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private StatisticsPanelModel Model {
		get;
		set;
	}

	private StatisticsPanelView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void RefreshPanel(PlayerStatisticsController playerStatistics)
	{
		View.RefreshView(playerStatistics.CurrentHealthPoints, playerStatistics.CurrentShieldPoints);
	}

	public void RegisterShieldPoints(IntValue shieldsPoints)
	{
		View.RegisterShieldPoints(shieldsPoints);
	}

	public void RegisterHealthPoints(IntValue healthPoints)
	{
		View.RegisterHealthPoints(healthPoints);
	}

	public void UnregisterShieldPoints()
	{
		View.UnregisterShieldPoints();
	}

	public void UnregisterHealthPoints()
	{
		View.UnregisterHealthPoints();
	}

	protected override void Awake()
	{
		base.Awake();
		PrepareProperties();
		AttachEvents();
	}

	protected virtual void OnDestroy()
    {
		DetachEvents();
    }

	private void PrepareProperties()
	{
		Model = GetModel<StatisticsPanelModel>();
		View = GetView<StatisticsPanelView>();
	}

	private void AttachEvents()
    {
		RegisterHealthPoints(PlayerManager.Instance.PlayerStatisticsController.HealthPoints);
		RegisterShieldPoints(PlayerManager.Instance.PlayerStatisticsController.ShieldsPoints);
    }

	private void DetachEvents()
    {
		UnregisterHealthPoints();
		UnregisterShieldPoints();
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
