using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
	public CenterPanelController CenterPanel => centerPanel;

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

	private void AttachEvents()
	{
		GameMainManager.Instance.OnGameOver += CenterPanel.ShowGameOverPanel;
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
