using Managers.GameManagers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ParticleEffect : BasePoolObject
{
    #region MEMBERS

    [FormerlySerializedAs("lifeTime")] [SerializeField]
    private float _lifeTime = 1.0f;
    [FormerlySerializedAs("particleSystemReference")] [SerializeField]
    private ParticleSystem _particleSystemReference = null;

    private IUpdateManager _updateManager;

    private Timer _lifeTimer;

    #endregion

    #region PROPERTIES

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
        _updateManager = updateManager;
    }

    private void AttachEvents()
    {
        OnHandleObjectSpawn += HandleLifeTime;
    }

    private void DetachEvents()
    {
        OnHandleObjectSpawn -= HandleLifeTime;

        if (_lifeTimer != null)
        {
            _lifeTimer.EndCounting();
        }
    }

    private void HandleLifeTime()
    {
        _lifeTimer = new Timer(_updateManager, 0, _lifeTime, Deactivation);
        _lifeTimer.StartCounting();
    }

    #endregion

    #region CLASS_ENUMS

    #endregion
}
