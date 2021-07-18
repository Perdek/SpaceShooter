using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPanelModel : Model
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public void BackToMenu()
    {
        LevelManager.Instance.EndLevel();
        GameMainManager.Instance.OpenMenu();
    }

    public void Continue()
    {
        LevelManager.Instance.EndLevel();
        GameMainManager.Instance.OpenWaitingRoom();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
