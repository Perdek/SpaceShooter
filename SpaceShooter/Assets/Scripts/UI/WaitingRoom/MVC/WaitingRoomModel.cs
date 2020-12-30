using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingRoomModel : Model
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    public PlayerStatisticsController GetPlayerStatistics()
    {
        return PlayerManager.Instance.PlayerStatisticsController;
    }

    public void Save()
    {

    }

    public void Ready()
    {
        GameMainManager.Instance.StartNextLevel();
    }

    public void Exit()
    {
        GameMainManager.Instance.OpenMenu();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
