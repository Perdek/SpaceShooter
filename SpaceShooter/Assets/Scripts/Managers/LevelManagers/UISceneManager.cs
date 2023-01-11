using System;
using Managers.GameManagers;
using Managers.LevelManagers;
using UnityEngine;
using Zenject;

public class UISceneManager : BaseMonoBehaviourSingletonManager<UISceneManager>
{
	#region MEMBERS

	[SerializeField]
	private StatisticsPanelController statisticPanel = null;
	[SerializeField]
	private LevelEndPanelController centerPanel = null;

	private IKeyboardManager _keyboardManager;
	private IPlayerManager _playerManager;
	private IGameMainManager _gameMainManager;
	private LevelEventsCommunicator _levelEventsCommunicator;

	#endregion

	#region PROPERTIES

	public StatisticsPanelController StatisticsPanel => statisticPanel;
	private LevelEndPanelController CenterPanel => centerPanel;

	private Guid KeyIdForOpenMenu {
		get;
		set;
	}

	#endregion

	#region METHODS

	[Inject]
	public void InjectDependencies(IKeyboardManager keyboardManager, IPlayerManager playerManager, IGameMainManager gameMainManager, LevelEventsCommunicator levelEventsCommunicator)
	{
		_keyboardManager = keyboardManager;
		_playerManager = playerManager;
		_gameMainManager = gameMainManager;
		_levelEventsCommunicator = levelEventsCommunicator;
	}

	public void RefreshUI()
	{
		StatisticsPanel.RefreshPanel(_playerManager.PlayerStatisticsController);
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
		_gameMainManager.OnGameOver += CenterPanel.ShowGameOver;
		KeyIdForOpenMenu = _keyboardManager.AddKey(KeyCode.Escape, OpenLevelMenu);
		_levelEventsCommunicator.OnLevelEnd += CenterPanel.ShowLevelEndPanel;
	}

	public void DetachEvents()
	{
		_gameMainManager.OnGameOver -= CenterPanel.ShowGameOver;
		_keyboardManager.RemoveKey(KeyIdForOpenMenu);
		_levelEventsCommunicator.OnLevelEnd -= CenterPanel.ShowLevelEndPanel;
	}

	private void OpenLevelMenu()
	{
		CenterPanel.ShowLevelEndPanel();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
