using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneManager : BaseMonoBehaviourSingletonManager<UISceneManager>
{
	#region MEMBERS

	[SerializeField]
	private TopRightPanelController topRightPanel = null;

	#endregion

	#region PROPERTIES

	private TopRightPanelController TopRightPanel => topRightPanel;

	#endregion

	#region METHODS

	public void RefreshUI()
	{
		TopRightPanel.RefreshPanel(PlayerManager.Instance.GetPlayerStatistics());
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
