using Managers.GameManagers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
	#region FIELDS

	public event Action<IBasePoolObject> OnSpawn = delegate { };
	public event Action OnSpawnEnd = delegate { };

	[SerializeField]
	private SpawnableObjectsTagsEnum poolObjectTag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;

	[SerializeField]
	private float firstSpawnDelayInSeconds = 0f;

	[SerializeField]
	private float delayBetweenSpawnsInSeconds = 5f;

	[SerializeField]
	private int spawnObjectsLimit = 1;

	private IPoolManager poolManager;
	private IUpdateManager updateManager;

	#endregion

	#region PROPERTIES

	public SpawnableObjectsTagsEnum PoolObjectTag => poolObjectTag;
	public float FirstSpawnDelayInSeconds => firstSpawnDelayInSeconds;
	public float DelayBetweenSpawnsInSeconds => delayBetweenSpawnsInSeconds;
	public int SpawnObjectsLimit => spawnObjectsLimit;

	private Timer SpawningTimer {
		get;
		set;
	} = null;

	private int CurrentSpawnedObjectNumber {
		get;
		set;
	} = 0;

	private List<IBasePoolObject> ActiveSpawnedObjects {
		get;
		set;
	} = new List<IBasePoolObject>();

    #endregion

    #region METHODS

    [Inject]
	public void InjectDependencies(IPoolManager poolManager, IUpdateManager updateManager)
    {
		this.poolManager = poolManager;
		this.updateManager = updateManager;
    }

	public void StartSpawn()
	{
		SpawningTimer = new Timer(updateManager, FirstSpawnDelayInSeconds, DelayBetweenSpawnsInSeconds, Spawn, true);
		SpawningTimer.StartCounting();
	}

	public void Spawn()
	{
		IBasePoolObject basePoolObject = poolManager.GetPoolObject(PoolObjectTag, this.transform.position, this.transform.rotation);
        basePoolObject.OnDeactivation += DeactiveSpawnedObject;
		CurrentSpawnedObjectNumber++;
		ActiveSpawnedObjects.Add(basePoolObject);
		OnSpawn(basePoolObject);
		HandleSpawnEnd();
	}

    public void Reset()
	{
		SpawningTimer.EndCounting();
		DeactivateSpawnedObjects();
	}

	private void DeactivateSpawnedObjects()
    {
        for (int i = ActiveSpawnedObjects.Count - 1; i >= 0; i--)
        {
			ActiveSpawnedObjects[i].Deactivation();
		}
    }

	private void HandleSpawnEnd()
	{
		if (CheckSpawnEnd() == true)
		{
			SpawningTimer.EndCounting();
			OnSpawnEnd();
		}
	}

	private bool CheckSpawnEnd()
	{
		return CurrentSpawnedObjectNumber == SpawnObjectsLimit;
	}

	private void DeactiveSpawnedObject(BasePoolObject obj)
	{
		obj.OnDeactivation -= DeactiveSpawnedObject;
		ActiveSpawnedObjects.Remove(obj);
	}

	#endregion

	#region ENUMS

	#endregion
}
