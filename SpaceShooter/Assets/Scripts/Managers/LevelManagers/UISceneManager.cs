using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UISceneManager : BaseMonoBehaviourSingletonManager<UISceneManager>
{
	#region MEMBERS

	[SerializeField]
	private TopRightPanelController topRightPanel = null;
	[SerializeField]
	private CenterPanelController centerPanel = null;

	#endregion

	#region PROPERTIES

	private TopRightPanelController TopRightPanel => topRightPanel;
	private CenterPanelController CenterPanel => centerPanel;

	private int KeyIdForOpenMenu {
		get;
		set;
	} = 0;

	#endregion

	#region METHODS

	public void RefreshUI()
	{
		TopRightPanel.RefreshPanel(PlayerManager.Instance.GetPlayerStatistics());
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
		UpdateManager.Instance.PauseTime();
		CenterPanel.ShowGameOverPanel();
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
