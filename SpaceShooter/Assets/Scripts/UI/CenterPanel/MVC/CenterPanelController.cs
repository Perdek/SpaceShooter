using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPanelController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private CenterPanelModel Model {
		get;
		set;
	}

	private CenterPanelView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void AttachEvents()
	{
		GameMainManager.Instance.OnGameOver += View.ShowCenterPanel;
	}

	public void DetachEvents()
	{
		if (GameMainManager.IsInstantiated == true)
		{
			GameMainManager.Instance.OnGameOver -= View.ShowCenterPanel;
		}
	}

	public bool IsShowedCenterPanel()
	{
		return View.IsShowedCenterPanel();
	}

	public void ShowGameOverPanel()
	{
		View.ShowCenterPanel();
	}

	public void BackToMenu()
	{
		Model.BackToMenu();
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
		Model = GetModel<CenterPanelModel>();
		View = GetView<CenterPanelView>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
