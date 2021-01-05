using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    #region FIELDS

    [Header("Controllers")]
    [SerializeField]
    private PlayerMovementController playerMovementController = null;
    [SerializeField]
    private PlayerShootingController playerShootingController = null;
    [SerializeField]
    private PlayerColliderController playerColliderController = null;
    [SerializeField]
    private PlayerStatisticsController playerStatisticsController = null;
    [SerializeField]
    private PlayerVisualisationController playerVisualisationController = null;

    #endregion

    #region PROPERTIES

    public PlayerMovementController PlayerMovementController => playerMovementController;
    public PlayerShootingController PlayerShootingController => playerShootingController;
    public PlayerColliderController PlayerColliderController => playerColliderController;
    public PlayerStatisticsController PlayerStatisticsController => playerStatisticsController;
    public PlayerVisualisationController PlayerVisualisationController => playerVisualisationController;

    #endregion

    #region METHODS

    public void Initialize()
    {
        AttachEvents();
        PlayerMovementController.Initialize();
        PlayerShootingController.Initialize();
    }

    public bool CanPlayerAffordCost(int value)
    {
        return PlayerStatisticsController.CurrentMoneyPoints >= value;
    }

    private void AttachEvents()
    {
        GameMainManager.Instance.OnGameStart += AttachInterControllersEvents;
        GameMainManager.Instance.OnGameStart += RefreshView;
        GameMainManager.Instance.OnMainOpen += DetachInterControllersEvents;
        GameMainManager.Instance.OnWaitingOpen += DetachInterControllersEvents;
        PlayerStatisticsController.OnPlayerDead += GameMainManager.Instance.GameOver;
    }

    private void RefreshView()
    {
        if(PlayerStatisticsController.CurrentShieldPoints > 0)
        {
            PlayerVisualisationController.TurnOnForceShield();
        }
    }

    private void AttachInterControllersEvents()
    {
        PlayerColliderController.OnDamageCollision += PlayerStatisticsController.HandleDamage;
        PlayerShootingController.OnKillEnemy += PlayerStatisticsController.RewardForKill;
        PlayerStatisticsController.OnTurnOnForceShield += PlayerVisualisationController.TurnOnForceShield;
        PlayerStatisticsController.OnTurnOffForceShield += PlayerVisualisationController.TurnOffForceShield;
    }

    private void DetachInterControllersEvents()
    {
        PlayerColliderController.OnDamageCollision -= PlayerStatisticsController.HandleDamage;
        PlayerShootingController.OnKillEnemy -= PlayerStatisticsController.RewardForKill;
        PlayerStatisticsController.OnTurnOnForceShield -= PlayerVisualisationController.TurnOnForceShield;
        PlayerStatisticsController.OnTurnOffForceShield -= PlayerVisualisationController.TurnOffForceShield;
    }

    #endregion

    #region ENUMS

    #endregion
}
