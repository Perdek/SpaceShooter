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

    }

    public void Exit()
    {

    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
