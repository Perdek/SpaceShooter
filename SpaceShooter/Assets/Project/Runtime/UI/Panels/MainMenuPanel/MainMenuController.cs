using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : Controller
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	private MainMenuModel Model {
		get;
		set;
	}

	private MainMenuView View {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void StartGame()
	{
		Model.StartGame();
	}

	public void Load()
	{

	}

	public void Exit()
	{
		Application.Quit();
	}

	protected override void Awake()
	{
		base.Awake();
		PrepareProperties();
	}

	private void PrepareProperties()
	{
		Model = GetModel<MainMenuModel>();
		View = GetView<MainMenuView>();
	}

	#endregion

	#region ENUMS

	#endregion
}
