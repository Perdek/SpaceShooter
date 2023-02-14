using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Managers.GameManagers
{

    public class PoolManager : MonoBehaviour, IPoolManager, IInitializable
    {
        #region FIELDS

        [SerializeField]
        private List<Pool> poolList = new List<Pool>();

        [SerializeField]
        private PoolObjectsParent poolObjectsParentPrefab = null;

        private BasePoolObjectsFactory basePoolObjectsFactory = null;
        private PoolObjectsParentFactory poolObjectsParentPrefabFactory = null;

        #endregion

        #region PROPERTIES

        public List<Pool> PoolList {
            get => poolList;
            private set => poolList = value;
        }

        public PoolObjectsParent PoolObjectsParentPrefab => poolObjectsParentPrefab;

        public Dictionary<string, Queue<IBasePoolObject>> PoolDictionary {
            get;
            private set;
        } = new Dictionary<string, Queue<IBasePoolObject>>();

        public Pool Pool {
            get => default;
            set {
            }
        }

        public PoolObjectsParent PoolObjectsParent {
            get => default;
            set {
            }
        }


        #endregion

        #region METHODS

        [Inject]
        public void InjectDependecies(BasePoolObjectsFactory basePoolObjectsFactory, PoolObjectsParentFactory poolObjectsParentPrefabFactory)
        {
            this.basePoolObjectsFactory = basePoolObjectsFactory;
            this.poolObjectsParentPrefabFactory = poolObjectsParentPrefabFactory;
        }

        public void Initialize()
        {
            InitializePools();
        }

        public void DeactivateAllObjects()
        {
            foreach (KeyValuePair<string, Queue<IBasePoolObject>> pool in PoolDictionary)
            {
                foreach (BasePoolObject basePoolObject in pool.Value)
                {
                    basePoolObject.Deactivation();
                }
            }
        }

        public IBasePoolObject GetPoolObject(SpawnableObjectsTagsEnum tag, Vector3 newPosition, Quaternion newRotation)
        {
            if (PoolDictionary.ContainsKey(tag.ToString()) == false)
            {
                return null;
            }

            IBasePoolObject poolObject = PoolDictionary[tag.ToString()].Dequeue();

            if (poolObject == null)
            {
                return null;
            }

            SetPoolObject(poolObject, newPosition, newRotation);

            PoolDictionary[tag.ToString()].Enqueue(poolObject);

            return poolObject;
        }

        private void SetPoolObject(IBasePoolObject poolObject, Vector3 newPosition, Quaternion newRotation)
        {
            poolObject.GetGameObject.transform.position = newPosition;
            poolObject.GetGameObject.transform.rotation = newRotation;
            poolObject.GetGameObject.gameObject.SetActive(true);
            poolObject.HandleObjectSpawn();
            poolObject.SetState(IBasePoolObject.PoolObjectStateEnum.IN_USE);
        }

        private void SetPoolObjectsParent(PoolObjectsParent newPoolObjectsParent, Pool pool)
        {
            newPoolObjectsParent.name = pool.Tag.ToString();
            newPoolObjectsParent.SetTag(pool.Tag.ToString());
            newPoolObjectsParent.transform.SetParent(this.transform);
        }

        private void InitializePools()
        {
            PoolObjectsParent poolObjectsParent = null;
            Pool pool = null;

            for (int i = 0; i < PoolList.Count; i++)
            {
                Queue<IBasePoolObject> poolQueue = new Queue<IBasePoolObject>();
                pool = PoolList[i];

                poolObjectsParent = poolObjectsParentPrefabFactory.Create(PoolObjectsParentPrefab, this.transform);
                SetPoolObjectsParent(poolObjectsParent, pool);

                for (int j = 0; j < pool.Size; j++)
                {
                    IBasePoolObject poolObject = basePoolObjectsFactory.Create(pool.PoolPrefab, poolObjectsParent.transform);
                    poolObject.GetGameObject.SetActive(false);
                    poolObject.SetState(IBasePoolObject.PoolObjectStateEnum.WAITING_FOR_USE);
                    poolQueue.Enqueue(poolObject);
                }

                PoolDictionary.Add(pool.Tag.ToString(), poolQueue);
            }
        }

        #endregion

        #region ENUMS

        #endregion
    }
}
