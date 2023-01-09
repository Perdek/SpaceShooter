using System;
using Managers.GameManagers;
using UnityEngine;
using Zenject;

public class UISceneManager : BaseMonoBehaviourSingletonManager<UISceneManager>
{
	#region MEMBERS

	[SerializeField]
	private StatisticsPanelController statisticPanel = null;
	[SerializeField]
	private LevelEndPanelController centerPanel = null;    

	#endregion

	#region PROPERTIES

	public StatisticsPanelController StatisticsPanel => statisticPanel;
	private LevelEndPanelController CenterPanel => centerPanel;

	[Inject]
	private IKeyboardManager keyboardManager;
	[Inject]
	private IPlayerManager playerManager;
    [Inject]
	private IGameMainManager gameMainManager;

	private Guid KeyIdForOpenMenu {
		get;
		set;
	}

	#endregion

	#region METHODS

	public void RefreshUI()
	{
		StatisticsPanel.RefreshPanel(playerManager.PlayerStatisticsController);
	}

	protected void Awake()
	{
		AttachEvents();
	}

	protected void OnDestroy()
	{
		DetachEvents();
	}

    private void AttachEvents()
	{
		gameMainManager.OnGameOver += CenterPanel.ShowGameOver;
		KeyIdForOpenMenu = keyboardManager.AddKey(KeyCode.Escape, OpenLevelMenu);
		LevelManager.Instance.OnLevelEnd += CenterPanel.ShowLevelEndPanel;
	}

	public void DetachEvents()
	{
		gameMainManager.OnGameOver -= CenterPanel.ShowGameOver;

		keyboardManager.RemoveKey(KeyIdForOpenMenu);

		if (LevelManager.IsInstantiated == true)
		{
			LevelManager.Instance.OnLevelEnd -= CenterPanel.ShowLevelEndPanel;
		}
	}

	private void OpenLevelMenu()
	{
		CenterPanel.ShowLevelEndPanel();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
