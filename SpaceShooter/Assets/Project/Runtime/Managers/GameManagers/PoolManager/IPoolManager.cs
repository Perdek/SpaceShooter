using System.Collections.Generic;
using UnityEngine;

namespace Managers.GameManagers
{
    public interface IPoolManager
    {
        #region PROPERTIES

        public List<Pool> PoolList { get; }
        public PoolObjectsParent PoolObjectsParentPrefab { get; }
        public Dictionary<string, Queue<IBasePoolObject>> PoolDictionary { get; }
        public Pool Pool { get; }
        public PoolObjectsParent PoolObjectsParent { get; }

        #endregion

        #region METHODS
        public void DeactivateAllObjects();
        public IBasePoolObject GetPoolObject(SpawnableObjectsTagsEnum tag, Vector3 newPosition, Quaternion newRotation);

        #endregion

    }
}
