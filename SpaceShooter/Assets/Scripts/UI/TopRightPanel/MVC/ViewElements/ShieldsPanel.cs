using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShieldsPanel
{
	#region MEMBERS

	[SerializeField]
	private Transform shieldsContentTransform = null;
	[SerializeField]
	private GameObject shieldsUIElementPrefab = null;

	#endregion

	#region PROPERTIES

	private Transform ShieldsContentTransform => shieldsContentTransform;
	private GameObject ShieldsUIElementPrefab => shieldsUIElementPrefab;

	private List<GameObject> ShieldsUIElements {
		get;
		set;
	} = new List<GameObject>();

	#endregion

	#region METHODS

	public void RefreshPanel(int playerShields)
	{
		ClearPanel();
		FillPanel(playerShields);
	}

	public void AttachEvents()
	{
		if (PlayerManager.Instance != null && PlayerManager.Instance.PlayerMainController != null)
		{
			PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnShieldsPointsAdd += AddUIElement;
			PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnShieldsPointsRemove += RemoveUIElement;
		}
	}

	public void DetachEvents()
	{
		if (PlayerManager.Instance != null && PlayerManager.Instance.PlayerMainController != null)
		{
			PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnShieldsPointsAdd -= AddUIElement;
			PlayerManager.Instance.PlayerMainController.PlayerStatisticsController.OnShieldsPointsRemove -= RemoveUIElement;
		}
	}

	private void ClearPanel()
	{
		for (int i = ShieldsUIElements.Count - 1; i >= 0; i--)
		{
			RemoveUIElement(ShieldsUIElements[i]);
		}
	}

	private void RemoveUIElement()
	{
		GameObject.Destroy(ShieldsUIElements[ShieldsUIElements.Count - 1].gameObject);
		ShieldsUIElements.Remove(ShieldsUIElements[ShieldsUIElements.Count - 1].gameObject);
	}

	private void RemoveUIElement(GameObject uiElement)
	{
		GameObject.Destroy(uiElement.gameObject);
		ShieldsUIElements.Remove(uiElement);
	}

	private void FillPanel(int playerShields)
	{
		for (int i = 0; i < playerShields; i++)
		{
			AddUIElement();
		}
	}

	private void AddUIElement()
	{
		GameObject newUIElement = GameObject.Instantiate(ShieldsUIElementPrefab, ShieldsContentTransform);
		newUIElement.SetActive(true);
		ShieldsUIElements.Add(newUIElement);
	}

	#endregion

	#region CLASS_ENUMS

	#endregion
}
