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

        View.AddListenerToUpgradeHP(UpgradeHP);
        View.AddListenerToUpgradeShield(UpgradeShield);

        View.RefreshHPButtonInteractivity(Model.CanPlayerAffordUpgradingHP());
        View.RefreshShieldButtonInteractivity(Model.CanPlayerAffordUpgradingShield());

        View.RefreshCosts(Model.GetBasicHealthCosts());
    }

    private void UpgradeHP()
    {
        Model.UpgradeHP();
        View.RefreshHPButtonInteractivity(Model.CanPlayerAffordUpgradingHP());
    }

    private void UpgradeShield()
    {
        Model.UpgradeShield();
        View.RefreshShieldButtonInteractivity(Model.CanPlayerAffordUpgradingShield());
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
