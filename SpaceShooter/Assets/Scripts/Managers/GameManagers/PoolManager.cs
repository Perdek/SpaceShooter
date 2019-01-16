using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : BaseMonoBehaviourSingletonManager<PoolManager>
{
	#region FIELDS

	[SerializeField]
	private List<Pool> poolList = new List<Pool>();

	[SerializeField]
	private PoolObjectsParent poolObjectsParentPrefab = null;

	#endregion

	#region PROPERTIES

	public List<Pool> PoolList {
		get => poolList;
		private set => poolList = value;
	}

	public PoolObjectsParent PoolObjectsParentPrefab => poolObjectsParentPrefab;

	public Dictionary<string, Queue<BasePoolObject>> PoolDictionary {
		get;
		private set;
	} = new Dictionary<string, Queue<BasePoolObject>>();


	#endregion

	#region METHODS

	public void Initialize()
	{
		Queue<BasePoolObject> poolQueue = new Queue<BasePoolObject>();
		PoolObjectsParent poolObjectsParent = null;
		Pool pool = null;

		for (int i = 0; i < PoolList.Count; i++)
		{
			pool = PoolList[i];
			poolQueue.Clear();
			poolObjectsParent = GameObject.Instantiate(PoolObjectsParentPrefab, this.transform);
			SetPoolObjectsParent(poolObjectsParent, pool);

			for (int j = 0; j < pool.Size; j++)
			{
				BasePoolObject poolObject = GameObject.Instantiate(pool.PoolPrefab, poolObjectsParent.transform);
				poolObject.gameObject.SetActive(false);
				poolQueue.Enqueue(poolObject);
			}

			PoolDictionary.Add(pool.Tag, poolQueue);
		}
	}

	public BasePoolObject GetPoolObject(string tag, Vector3 newPosition, Quaternion newRotation)
	{
		if (PoolDictionary.ContainsKey(tag) == false)
		{
			return null;
		}

		BasePoolObject poolObject = PoolDictionary[tag].Dequeue();

		if (poolObject == null)
		{
			return null;
		}

		SetPoolObject(poolObject, newPosition, newRotation);

		PoolDictionary[tag].Enqueue(poolObject);

		return poolObject;
	}

	private void SetPoolObject(BasePoolObject poolObject, Vector3 newPosition, Quaternion newRotation)
	{
		poolObject.transform.position = newPosition;
		poolObject.transform.rotation = newRotation;
		poolObject.gameObject.SetActive(true);
		poolObject.HandleObjectSpawn();
	}

	private void SetPoolObjectsParent(PoolObjectsParent newPoolObjectsParent, Pool pool)
	{
		newPoolObjectsParent.name = pool.Tag;
		newPoolObjectsParent.SetTag(pool.Tag);
		newPoolObjectsParent.transform.SetParent(this.transform);
	}

	#endregion

	#region ENUMS

	#endregion
}
