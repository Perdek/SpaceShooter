using UnityEngine;
using System.Collections;
using System;
using Zenject;
using Managers.GameManagers;

public class ParticleEffect : BasePoolObject
{
    #region MEMBERS

    [SerializeField]
    private float lifeTime = 1.0f;
    [SerializeField]
    private ParticleSystem particleSystemReference = null;

    private IUpdateManager updateManager;

    #endregion

    #region PROPERTIES

    private ParticleSystem ParticleSystemReference => particleSystemReference;
    private float LifeTime => lifeTime;

    private Timer LifeTimer {
        get;
        set;
    } = null;

    #endregion

    #region UNITY_METHODS

    protected virtual void Awake()
    {
        AttachEvents();
    }

    protected virtual void OnDestroy()
    {
        DetachEvents();
    }

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IUpdateManager updateManager)
    {
        this.updateManager = updateManager;
    }

    private void AttachEvents()
    {
        OnHandleObjectSpawn += HandleLifeTime;
    }

    private void DetachEvents()
    {
        OnHandleObjectSpawn -= HandleLifeTime;

        if (LifeTimer != null)
        {
            LifeTimer.EndCounting();
        }
    }

    private void HandleLifeTime()
    {
        LifeTimer = new Timer(updateManager, 0, LifeTime, Deactivation);
        LifeTimer.StartCounting();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
