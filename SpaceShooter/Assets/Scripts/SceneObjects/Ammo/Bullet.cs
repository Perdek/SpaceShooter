using UnityEngine;
using System;
using Zenject;
using Managers.GameManagers;
using SceneObjects.Ammo;
using static IBasePoolObject;

public class Bullet : BasePoolObject
{
    #region FIELDS

    private int id;

    public event Action<EnemyInformation> OnKillTarget = delegate { };

    [SerializeField]
    private uint speedFactory = 10;

    [SerializeField]
    private Rigidbody2D rigidbody2DComponent = null;

    [SerializeField]
    private IdentificationFriendOrFoeEnum iff = IdentificationFriendOrFoeEnum.FOE;

    protected IUpdateManager _updateManager;
    protected KillingCommunicator _killingCommunicator;

    #endregion

    #region PROPERTIES

    public Rigidbody2D Rigidbody2DComponent => rigidbody2DComponent;
    public uint SpeedFactory => speedFactory;
    public IdentificationFriendOrFoeEnum Iff => iff;

    #endregion

    #region UNITY_METHODS

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        HandleCollision(other);
    }

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IUpdateManager newUpdateManager, KillingCommunicator killingCommunicator)
    {
        _updateManager = newUpdateManager;
        _killingCommunicator = killingCommunicator;
    }

    public override void HandleObjectSpawn()
    {
        if (State == PoolObjectStateEnum.WAITING_FOR_USE)
        {
            _updateManager.OnUpdatePhysic += Move;
            OnKillTarget += _killingCommunicator.NotifyOnKillEnemy;
        }
    }

    public override void Deactivation()
    {
        base.Deactivation();
        OnKillTarget = null;

        _updateManager.OnUpdatePhysic -= Move;
    }

    public void NotifyComfirmKill(EnemyInformation enemyInformation)
    {
        if (OnKillTarget == null)
        {
            Debug.Log("OnKillTarget == null " + id);
        }
        else
        {
            OnKillTarget(enemyInformation);
        }
    }

    public void Move()
    {
        Vector2 localForwardVector = new Vector2(transform.up.x, transform.up.y);

        Rigidbody2DComponent.MovePosition(Rigidbody2DComponent.position + localForwardVector * Time.fixedDeltaTime * SpeedFactory);
    }

    private void HandleCollision(Collider2D hittedObjectCollider)
    {
        if (CheckCollisionWithPlayer(hittedObjectCollider) == true)
        {
            return;
        }

        Enemy enemy = hittedObjectCollider.GetComponentInChildren<Enemy>();

        if (enemy != null)
        {
            NotifyComfirmKill(enemy.EnemyInformation);
        }

        Deactivation();
    }

    private bool CheckCollisionWithPlayer(Collider2D other)
    {
        return other.GetComponentInChildren<PlayerColliderController>() != null;
    }

    #endregion

    #region ENUMS

    #endregion
}
