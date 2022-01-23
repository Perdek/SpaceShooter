using UnityEngine;

namespace SceneObjects.Bonuses
{
    public class BonusMovementComponent : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private uint speedFactory = 5;
        [SerializeField]
        private Rigidbody2D rigidbody2DComponent = null;
        [SerializeField]
        private Vector2 calculatedDirection = new Vector2(0,-1);

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        public void AttachEvents()
        {
            UpdateManager.Instance.OnUpdatePhysic += Move;
        }

        public void DetachEvents()
        {
            UpdateManager.Instance.OnUpdatePhysic -= Move;
        }

        private void Move()
        {
            rigidbody2DComponent.MovePosition(rigidbody2DComponent.position + calculatedDirection * Time.fixedDeltaTime * speedFactory);
        }

        #endregion

        #region ENUMS

        #endregion
    }
}