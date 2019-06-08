using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuModel : Model
{
	#region FIELDS

	#endregion

	#region PROPERTIES

	#endregion

	#region METHODS

	public void StartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene(ScenesNamesManager.Level01);
		GameMainManager.Instance.SetGameStateAsGame();
	}

	#endregion

	#region ENUMS

	#endregion
}
