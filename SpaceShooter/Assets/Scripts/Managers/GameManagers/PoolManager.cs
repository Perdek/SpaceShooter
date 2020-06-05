using System;
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

	public override void Initialize()
	{
		InitializePools();
	}

	public void DeactivateAllObjects()
	{
        foreach (KeyValuePair<string, Queue<BasePoolObject>> pool in PoolDictionary)
        {
            foreach (BasePoolObject basePoolObject in pool.Value)
            {
				basePoolObject.Deactivation();
			}
		}
	}

	public BasePoolObject GetPoolObject(TagManager.TagsEnum tag, Vector3 newPosition, Quaternion newRotation)
	{
		if (PoolDictionary.ContainsKey(tag.ToString()) == false)
		{
			return null;
		}

		BasePoolObject poolObject = PoolDictionary[tag.ToString()].Dequeue();

		if (poolObject == null)
		{
			return null;
		}

		SetPoolObject(poolObject, newPosition, newRotation);

		PoolDictionary[tag.ToString()].Enqueue(poolObject);

		return poolObject;
	}

	private void SetPoolObject(BasePoolObject poolObject, Vector3 newPosition, Quaternion newRotation)
	{
		poolObject.transform.position = newPosition;
		poolObject.transform.rotation = newRotation;
		poolObject.gameObject.SetActive(true);
		poolObject.HandleObjectSpawn();
		poolObject.SetState(BasePoolObject.PoolObjectStateEnum.IN_USE);
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
			Queue<BasePoolObject> poolQueue = new Queue<BasePoolObject>();
			pool = PoolList[i];

			poolObjectsParent = GameObject.Instantiate(PoolObjectsParentPrefab, this.transform);
			SetPoolObjectsParent(poolObjectsParent, pool);

			for (int j = 0; j < pool.Size; j++)
			{
				BasePoolObject poolObject = GameObject.Instantiate(pool.PoolPrefab, poolObjectsParent.transform);
				poolObject.gameObject.SetActive(false);
				poolObject.SetState(BasePoolObject.PoolObjectStateEnum.WAITING_FOR_USE);
				poolQueue.Enqueue(poolObject);
			}

			PoolDictionary.Add(pool.Tag.ToString(), poolQueue);
		}
	}

	#endregion

	#region ENUMS

	#endregion
}
