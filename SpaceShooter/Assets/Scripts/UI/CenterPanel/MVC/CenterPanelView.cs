using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CenterPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private Text centerPanelText = null;
	[SerializeField]
	private GameObject centerPanelGameObject = null;

	#endregion

	#region PROPERTIES

	private Text CenterPanelText => centerPanelText;
	private GameObject CenterPanelGameObject => centerPanelGameObject;

	#endregion

	#region METHODS

	public void ShowCenterPanel()
	{
		CenterPanelGameObject.SetActive(true);
	}

	public void HideCenterPanel()
	{
		CenterPanelGameObject.SetActive(false);
	}

	public bool IsShowedCenterPanel()
    {
		return CenterPanelGameObject.activeInHierarchy;
	}

    #endregion

    #region CLASS_ENUMS

    #endregion
}
