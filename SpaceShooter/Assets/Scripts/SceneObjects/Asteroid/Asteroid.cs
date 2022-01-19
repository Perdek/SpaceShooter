using UnityEngine;

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

    #endregion

    #region PROPERTIES

    private AsteroidCollisionComponent CollisionComponent => collisionComponent;
    private AsteroidMovementComponent MovementComponent => movementComponent;
    private AsteroidViewComponent ViewComponent => viewComponent;

    #endregion

    #region METHODS

    public void AttachEvents()
    {
        CollisionComponent.OnHit += Deactivation;

        if (isBreakable == true)
        {
            CollisionComponent.OnKillByPlayer += BreakAsteroid;
        }

        MovementComponent.AttachEvents();
    }

    public void DetachEvents()
    {
        CollisionComponent.OnHit -= Deactivation;

        MovementComponent.DetachEvents();
    }

    public override void HandleObjectSpawn()
    {
        base.HandleObjectSpawn();

        AttachEvents();
        Initialize();
    }

    public override void Deactivation()
    {
        base.Deactivation();

        Terminate();
        DetachEvents();
    }

    public void SetIsBreakable(bool newValue)
    {
        isBreakable = newValue;

        if (isBreakable == false)
        {
            CollisionComponent.OnKillByPlayer -= BreakAsteroid;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        CollisionComponent.HandleCollision(other);
    }

    private void Initialize()
    {
        MovementComponent.Initialize();
        ViewComponent.SetAsteroidTranform(this.transform);
        CollisionComponent.SetAsteroidInformation(EnemyInformation);
        isBreakable = true;
    }

    private void Terminate()
    {
        ViewComponent.Explosion();
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
        BasePoolObject basePoolObject = PoolManager.Instance.GetPoolObject(TagManager.TagsEnum.LITTLE_ASTEROID_TAG, this.transform.position, this.transform.rotation);
        Asteroid littleAsteroid = basePoolObject as Asteroid;
        littleAsteroid.SetIsBreakable(false);
    }

    #endregion

    #region ENUMS

    #endregion
}
