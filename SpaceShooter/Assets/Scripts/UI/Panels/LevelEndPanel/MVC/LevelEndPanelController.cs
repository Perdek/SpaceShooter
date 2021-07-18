using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPanelController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private LevelEndPanelModel Model {
		get;
		set;
	}

	private LevelEndPanelView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void AttachEvents()
	{
		GameMainManager.Instance.OnGameOver += View.ShowEndLevelPanel;
	}

	public void DetachEvents()
	{
		if (GameMainManager.IsInstantiated == true)
		{
			GameMainManager.Instance.OnGameOver -= View.ShowEndLevelPanel;
		}
	}

	public bool IsShowedCenterPanel()
	{
		return View.IsShowedLevelEndPanel();
	}

	public void ShowLevelEndPanel()
	{
		UpdateManager.Instance.PauseTime();
		View.ShowEndLevelPanel();
	}

	public void ShowGameOver()
    {
		UpdateManager.Instance.PauseTime();
		View.ShowGameOverPanel();
	}

	protected virtual void OnDestroy()
	{
		DetachEvents();
	}

	public override void Initialize()
	{
		base.Initialize();


		PrepareProperties();
		AttachEvents();

		View.AddListenerToContinueButton(Model.Continue);
		View.AddListenerToMenuButton(Model.BackToMenu);
	}

	private void PrepareProperties()
	{
		Model = GetModel<LevelEndPanelModel>();
		View = GetView<LevelEndPanelView>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
