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
	}

	private void PrepareProperties()
	{
		Model = GetModel<TopLeftPanelModel>();
		View = GetView<TopLeftPanelView>();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
