using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HPPanel
{
	#region MEMBERS

	[SerializeField]
	private Transform hpContentTransform = null;
	[SerializeField]
	private GameObject hpUIElementPrefab = null;

	#endregion

	#region PROPERTIES

	private Transform HpContentTransform => hpContentTransform;
	private GameObject HPUIElementPrefab => hpUIElementPrefab;

	private List<GameObject> HPUIElements {
		get;
		set;
	} = new List<GameObject>();

	#endregion

	#region METHODS

	public void RefreshPanel(int playerHP)
	{
		ClearPanel();
		FillPanel(playerHP);
	}

	public void AttachEvents()
	{
		PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnHealthPointsAdd += AddUIElement;
		PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnHealthPointsRemove += RemoveUIElement;
	}

	public void DetachEvents()
	{
		PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnHealthPointsAdd -= AddUIElement;
		PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnHealthPointsRemove -= RemoveUIElement;
	}

	private void ClearPanel()
	{
		for (int i = HPUIElements.Count - 1; i >= 0; i--)
		{
			RemoveUIElement(HPUIElements[i]);
		}
	}

	private void RemoveUIElement()
	{
		GameObject.Destroy(HPUIElements[HPUIElements.Count - 1].gameObject);
		HPUIElements.Remove(HPUIElements[HPUIElements.Count - 1].gameObject);
	}

	private void RemoveUIElement(GameObject uiElement)
	{
		GameObject.Destroy(uiElement.gameObject);
		HPUIElements.Remove(uiElement);
	}

	private void FillPanel(int playerHP)
	{
		for (int i = 0; i < playerHP; i++)
		{
			AddUIElement();
		}
	}

	private void AddUIElement()
	{
		GameObject newUIElement = GameObject.Instantiate(HPUIElementPrefab, HpContentTransform);
		newUIElement.SetActive(true);
		HPUIElements.Add(newUIElement);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
