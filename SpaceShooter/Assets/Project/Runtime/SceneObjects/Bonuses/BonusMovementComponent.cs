using Managers.GameManagers;
using UnityEngine;
using Zenject;

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

        private IUpdateManager updateManager;

        #endregion

        #region PROPERTIES

        #endregion

        #region METHODS

        [Inject]
        public void InjectDependencies(IUpdateManager newUpdateManager)
        {
            updateManager = newUpdateManager;
        }

        public void AttachEvents()
        {
            updateManager.OnUpdatePhysic += Move;
        }

        public void DetachEvents()
        {
            updateManager.OnUpdatePhysic -= Move;
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