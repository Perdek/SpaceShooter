using UnityEngine;
using System;
using System.Collections.Generic;

public class HomingMissile : Bullet
{
    #region MEMBERS

    private const float DEFAULT_HOMING_MISSILE_RANGE = 50;

    [SerializeField] private float radarRange = DEFAULT_HOMING_MISSILE_RANGE;
    [SerializeField] private float rotateSpeed = 200f;

    #endregion

    #region PROPERTIES

    private float RadarRange => radarRange;
    private float RotateSpeed => rotateSpeed;

    private Enemy TrackedOpponent {
        get;
        set;
    } = default;

    #endregion

    #region METHODS

    public override void HandleObjectSpawn()
    {
        base.HandleObjectSpawn();

        UpdateManager.Instance.OnUpdatePhysic += TrackOpponent;
    }

    public override void Deactivation()
    {
        base.Deactivation();

        UpdateManager.Instance.OnUpdatePhysic -= TrackOpponent;
    }

    private void TrackOpponent()
    {
        if (IsAnyEnemyTracking() == true)
        {
            GuidToTrackedEnemy();
        }
        else
        {
            FindEnemyToTrack();
        }
    }

    private bool IsAnyEnemyTracking()
    {
        return TrackedOpponent != null && TrackedOpponent.State == PoolObjectStateEnum.WAITING_FOR_USE;
    }

    private void GuidToTrackedEnemy()
    {
        Vector2 direction = TrackedOpponent.gameObject.transform.position - transform.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        Rigidbody2DComponent.angularVelocity = -rotateAmount * RotateSpeed;
    }

    private void FindEnemyToTrack()
    {
        RaycastHit2D[] trackedObjects = Physics2D.CircleCastAll(transform.position, RadarRange, Vector2.zero);
        List<Enemy> foundEnemies = new List<Enemy>();

        for (int i = 0; i < trackedObjects.Length; i++)
        {
            Enemy enemy = trackedObjects[i].collider.GetComponent<Enemy>();

            if (enemy != null)
            {
                foundEnemies.Add(enemy);
            }
        }

        float closestDistanceToEnemy = float.MaxValue;

        for (int i = 0; i < foundEnemies.Count; i++)
        {
            float distance = Vector2.Distance(transform.position, foundEnemies[i].transform.position);

            if (distance < closestDistanceToEnemy)
            {
                closestDistanceToEnemy = distance;
                TrackedOpponent = foundEnemies[i];
            }
        }
    }

    #endregion
}
