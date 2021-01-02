using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftPanelController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private TopLeftPanelModel Model {
		get;
		set;
	}

	private TopLeftPanelView View {
		get;
		set;
	}

	#endregion

	#region FUNCTIONS

	protected override void Awake()
	{
		base.Awake();
		PrepareProperties();
		AttachEvents();
	}

	private void PrepareProperties()
	{
		Model = GetModel<TopLeftPanelModel>();
		View = GetView<TopLeftPanelView>();
	}

	private void AttachEvents()
    {
		View.RegisterMoney(Model.GetMoneyParameter());
		View.RegisterScore(Model.GetScoreParameter());
		View.RegisterWeapon(Model.GetWeaponParameter());
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
