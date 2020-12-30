using UnityEngine.Events;
using UnityEngine;
using UI.WaitingRoom;

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

    public void AddListenerToSaveButton(UnityAction onClick)
    {
        ButtonsPanel.AddListenerToSaveButton(onClick);
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
