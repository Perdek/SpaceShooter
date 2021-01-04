using UnityEngine.Events;
using UnityEngine;
using UI.WaitingRoom;
using System;

public class WaitingRoomView : View
{
	#region MEMBERS

	[SerializeField] private UI.WaitingRoom.WeaponPanel weaponPanel = default;
	[SerializeField] private UI.WaitingRoom.StatisticsPanel statisticsPanel = default;
	[SerializeField] private UI.WaitingRoom.ButtonsPanel buttonsPanel = default;

	#endregion

	#region PROPERTIES

	private UI.WaitingRoom.WeaponPanel WeaponPanel => weaponPanel;
	private UI.WaitingRoom.StatisticsPanel StatisticsPanel => statisticsPanel;
	private UI.WaitingRoom.ButtonsPanel ButtonsPanel => buttonsPanel;

	#endregion

	#region METHODS

	public void RefreshWeaponPanel()
	{

	}

	public void RefreshStatisticsPanel(PlayerStatisticsController playerStatisticsController)
	{
		StatisticsPanel.RegisterPlayerStatistics(playerStatisticsController);
	}

	public void AddListenerToExitButton(UnityAction onClick)
	{
		ButtonsPanel.AddListenerToExitButton(onClick);
	}

	public void AddListenerToReadyButton(UnityAction onClick)
	{
		ButtonsPanel.AddListenerToReadyButton(onClick);
	}

	public void RefreshCosts(ShopCostsInformation shopCostsInformation)
    {
		StatisticsPanel.RefreshCosts(shopCostsInformation);
	}

    public void AddListenerToSaveButton(UnityAction onClick)
	{
		ButtonsPanel.AddListenerToSaveButton(onClick);
	}

	public void AddListenerToUpgradeHP(UnityAction onClick)
	{
        StatisticsPanel.AddListenerToUpgradeHP(onClick);
	}

	public void AddListenerToUpgradeShield(UnityAction onClick)
	{
        StatisticsPanel.AddListenerToUpgradeShield(onClick);
	}

	public void RefreshShieldButtonInteractivity(bool value)
	{
		StatisticsPanel.RefreshShieldButtonInteractivity(value);
	}

	public void RefreshHPButtonInteractivity(bool value)
	{
		StatisticsPanel.RefreshHPButtonInteractivity(value);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
