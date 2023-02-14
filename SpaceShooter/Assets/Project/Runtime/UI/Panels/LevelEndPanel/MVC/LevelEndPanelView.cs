using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelEndPanelView : View
{
	#region MEMBERS

	[SerializeField]
	private Text centerPanelText = null;
	[SerializeField]
	private GameObject centerPanelGameObject = null;
	[SerializeField]
	private Button continueButton = null;
	[SerializeField]
	private Button menuButton = null;

	#endregion

	#region PROPERTIES

	private Text CenterPanelText => centerPanelText;
	private GameObject LevelEndGameObject => centerPanelGameObject;
	private Button ContinueButton => continueButton;
	private Button MenuButton => menuButton;

    #endregion

    #region METHODS

	public void AddListenerToContinueButton(UnityAction onClick)
	{
		ContinueButton.onClick.AddListener(onClick);
	}

	public void AddListenerToMenuButton(UnityAction onClick)
	{
		MenuButton.onClick.AddListener(onClick);
	}

    public void ShowEndLevelPanel()
	{
		LevelEndGameObject.SetActive(true);
		SetTextForEndLevel();

		ContinueButton.interactable = true;
	}

	public void ShowGameOverPanel()
	{
		LevelEndGameObject.SetActive(true);
		SetTextForGameOver();

		ContinueButton.interactable = false;
	}

	public void HideCenterPanel()
	{
		LevelEndGameObject.SetActive(false);
	}

	public bool IsShowedLevelEndPanel()
    {
		return LevelEndGameObject.activeInHierarchy;
	}

	public void SetTextForGameOver()
    {
		CenterPanelText.text = "Game Over";
	}

	public void SetTextForEndLevel()
    {
		CenterPanelText.text = "Level End";
	}

    #endregion

    #region CLASS_ENUMS

    #endregion
}
