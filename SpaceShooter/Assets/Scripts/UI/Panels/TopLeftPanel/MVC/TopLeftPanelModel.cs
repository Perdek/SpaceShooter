using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopLeftPanelModel : Model
{
    #region MEMBERS

    #endregion

    #region PROPERTIES

    #endregion

    #region FUNCTIONS

    public IntValue GetMoneyParameter()
    {
        return PlayerManager.Instance.PlayerStatisticsController.MoneyPoints;
    }

    public IntValue GetScoreParameter()
    {
        return PlayerManager.Instance.PlayerStatisticsController.ScorePoints;
    }

    public WeaponValue GetWeaponParameter()
    {
        return PlayerManager.Instance.PlayerShootingController.ActiveWeapon;
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
