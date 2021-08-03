using UnityEngine.Events;
using UnityEngine;
using UI.WaitingRoom;

public class WaitingRoomView : View
{
	#region MEMBERS

	[SerializeField] private StatisticsPanel statisticsPanel = default;
	[SerializeField] private WeaponPanel weaponPanel = default;
	[SerializeField] private ButtonsPanel buttonsPanel = default;

	#endregion

	#region PROPERTIES

	private StatisticsPanel StatisticsPanel => statisticsPanel;
	private WeaponPanel WeaponPanel => weaponPanel;
	private ButtonsPanel ButtonsPanel => buttonsPanel;

	#endregion

	#region METHODS

	public void RefreshWeaponPanel(PlayerShootingController playerShootingController)
	{
		WeaponPanel.RefreshPanel(playerShootingController.Weapons);
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

	protected virtual void OnDestroy()
    {
		WeaponPanel.ClearPanel();
    }

	#endregion

	#region CLASS_ENUMS

	#endregion
}
