using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(WaitingRoomModel), typeof(WaitingRoomView))]
public class WaitingRoomController : Controller
{
	#region MEMBERS

	#endregion

	#region PROPERTIES

	private WaitingRoomModel Model {
		get => GetModel<WaitingRoomModel>();
	}

	private WaitingRoomView View {
		get => GetView<WaitingRoomView>();
	}

    #endregion

    #region METHODS

    public override void Initialize()
    {
        base.Initialize();

        View.RefreshStatisticsPanel(Model.GetPlayerStatistics());
        View.AddListenerToSaveButton(Model.Save);
        View.AddListenerToReadyButton(Model.Ready);
        View.AddListenerToExitButton(Model.Exit);
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
