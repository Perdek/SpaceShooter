using SceneObjects.Bonuses;
using UnityEngine;

public class Bonus : BasePoolObject
{
    #region FIELDS

    [SerializeField]
    private BonusCollisionComponent collisionComponent = null;
    [SerializeField]
    private BonusMovementComponent movementComponent = null;

    #endregion

    #region PROPERTIES

    private BonusCollisionComponent CollisionComponent => collisionComponent;
    private BonusMovementComponent MovementComponent => movementComponent;

    #endregion

    #region METHODS

    public void AttachEvents()
    {
        CollisionComponent.OnHit += Deactivation;

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

        DetachEvents();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        CollisionComponent.HandleCollision(other);
    }

    private void Initialize()
    {
        MovementComponent.Initialize();
    }

    #endregion

    #region ENUMS

    #endregion
}
