using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPanelModel : Model
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

    #endregion

    #region CLASS_ENUMS

    #endregion
}
