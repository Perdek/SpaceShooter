using UnityEngine;
using System.Collections;
using Zenject;
using Managers.GameManagers;
using Managers.LevelManagers;

public class Asteroid : Enemy
{
    #region FIELDS

    [SerializeField]
    private AsteroidCollisionComponent collisionComponent = null;
    [SerializeField]
    private AsteroidMovementComponent movementComponent = null;
    [SerializeField]
    private AsteroidViewComponent viewComponent = null;

    private bool isBreakable = true;
    private IPoolManager poolManager;

    #endregion

    #region PROPERTIES

    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IPoolManager poolManager, IUpdateManager updateManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        viewComponent.InjectDependencies(poolManager, levelEventsCommunicator);
        movementComponent.InjectDependencies(updateManager);
        this.poolManager = poolManager;
    }

    public void AttachEvents()
    {
        collisionComponent.OnHit += Deactivation;

        if (isBreakable == true)
        {
            collisionComponent.OnKillByPlayer += HandleKillByPlayer;
        }

        movementComponent.AttachEvents();
    }

    public void DetachEvents()
    {
        collisionComponent.OnHit -= Deactivation;

        movementComponent.DetachEvents();
    }

    public override void HandleObjectSpawn()
    {
        base.HandleObjectSpawn();

        AttachEvents();
        Initialize();
        StartCoroutine(ActivateCollider());
    }

    private IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(1f);
        collisionComponent.ActivateCollider();
    }

    public override void Deactivation()
    {
        base.Deactivation();

        Terminate();
        DetachEvents();
        collisionComponent.DeactivateCollider();
    }

    public void SetIsBreakable(bool newValue)
    {
        isBreakable = newValue;

        if (isBreakable == false)
        {
            collisionComponent.OnKillByPlayer -= HandleKillByPlayer;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        collisionComponent.HandleCollision(other);
    }

    private void Initialize()
    {
        movementComponent.Initialize();
        viewComponent.SetAsteroidTransform(this.transform);
        collisionComponent.SetAsteroidInformation(EnemyInformation);
        isBreakable = true;
    }

    private void Terminate()
    {
        viewComponent.Explosion();
    }

    private void HandleKillByPlayer()
    {
        SpawnBonus();
        BreakAsteroid();
    }

    private void SpawnBonus()
    {
        if (isBreakable == false)
        {
            poolManager.GetPoolObject(SpawnableObjectsTagsEnum.HP_BONUS, this.transform.position, this.transform.rotation);
        }
    }

    private void BreakAsteroid()
    {
        int amountOfRandomParts = Random.Range(0, Constants.AMOUNT_OF_MAX_PARTS_OF_ASTEROID);

        for (int i = 0; i < amountOfRandomParts; i++)
        {
            SpawnOneLittleAsteroid();
        }
    }

    private void SpawnOneLittleAsteroid()
    {
        IBasePoolObject basePoolObject = poolManager.GetPoolObject(SpawnableObjectsTagsEnum.LITTLE_ASTEROID_TAG, this.transform.position, this.transform.rotation);
        Asteroid littleAsteroid = basePoolObject as Asteroid;
        littleAsteroid.SetIsBreakable(false);
    }

    #endregion

    #region ENUMS

    #endregion
}
