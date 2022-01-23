using System;
using UnityEngine;

namespace SceneObjects.Bonuses
{
    public class BonusCollisionComponent : MonoBehaviour
    {
        #region FIELDS

        public event Action OnHit = delegate { };

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void HandleCollision(Collider2D other)
        {
            Bullet bullet = other.GetComponent<Bullet>();

            if (bullet != null)
            {
                OnHit();
                return;
            }

            if (other.GetComponent<SceneBottonCollider>() != null)
            {
                OnHit();
            }

            PlayerColliderController playerCollider = other.GetComponentInChildren<PlayerColliderController>();

            if (playerCollider != null)
            {
                OnHit();
            }
        }

        #endregion

        #region ENUMS

        #endregion
    }
}