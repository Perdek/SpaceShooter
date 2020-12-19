using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UISceneManager : BaseMonoBehaviourSingletonManager<UISceneManager>
{
	#region MEMBERS

	[SerializeField]
	private StatisticsPanelController statisticPanel = null;
	[SerializeField]
	private CenterPanelController centerPanel = null;

	#endregion

	#region PROPERTIES

	public StatisticsPanelController StatisticsPanel => statisticPanel;
	private CenterPanelController CenterPanel => centerPanel;

	private int KeyIdForOpenMenu {
		get;
		set;
	} = 0;

	#endregion

	#region METHODS

	public void RefreshUI()
	{
		StatisticsPanel.RefreshPanel(PlayerManager.Instance.PlayerStatisticsController);
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
		GameMainManager.Instance.OnGameOver += CenterPanel.ShowGameOverPanel;
		KeyIdForOpenMenu = KeyboardManager.Instance.AddKey(KeyCode.Escape, OpenLevelMenu);
		LevelManager.Instance.OnLevelEnd += CenterPanel.ShowGameOverPanel;
	}

	public void DetachEvents()
	{
		if (GameMainManager.IsInstantiated == true)
		{
			GameMainManager.Instance.OnGameOver -= CenterPanel.ShowGameOverPanel;
		}

		if (KeyboardManager.IsInstantiated == true)
		{
			KeyboardManager.Instance.RemoveKey(KeyIdForOpenMenu);
		}

		if (LevelManager.IsInstantiated == true)
		{
			LevelManager.Instance.OnLevelEnd -= CenterPanel.ShowGameOverPanel;
		}
	}

	private void OpenLevelMenu()
	{
		CenterPanel.ShowGameOverPanel();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
