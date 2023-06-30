using Managers.GameManagers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
	#region FIELDS

	public event Action<IBasePoolObject> OnSpawn = delegate { };
	public event Action OnSpawnEnd = delegate { };

	[FormerlySerializedAs("poolObjectTag")] [SerializeField]
	private SpawnableObjectsTagsEnum _poolObjectTag = SpawnableObjectsTagsEnum.PLAYER_BULLET_TAG;

	[FormerlySerializedAs("firstSpawnDelayInSeconds")] [SerializeField]
	private float _firstSpawnDelayInSeconds = 0f;

	[FormerlySerializedAs("delayBetweenSpawnsInSeconds")] [SerializeField]
	private float _delayBetweenSpawnsInSeconds = 5f;

	[FormerlySerializedAs("spawnObjectsLimit")] [SerializeField]
	private int _spawnObjectsLimit = 1;

	private IPoolManager _poolManager;
	private IUpdateManager _updateManager;

	private Timer _spawningTimer;
	private int _currentSpawnedObjectNumber;
	private readonly List<IBasePoolObject> _activeSpawnedObjects = new List<IBasePoolObject>();

	#endregion

	#region PROPERTIES

	public SpawnableObjectsTagsEnum PoolObjectTag => _poolObjectTag;
	public float FirstSpawnDelayInSeconds => _firstSpawnDelayInSeconds;
	public float DelayBetweenSpawnsInSeconds => _delayBetweenSpawnsInSeconds;
	public int SpawnObjectsLimit => _spawnObjectsLimit;

	#endregion

    #region METHODS

    [Inject]
	public void InjectDependencies(IPoolManager poolManager, IUpdateManager updateManager)
    {
		this._poolManager = poolManager;
		this._updateManager = updateManager;
    }

	public void StartSpawn()
	{
		_spawningTimer = new Timer(_updateManager, FirstSpawnDelayInSeconds, DelayBetweenSpawnsInSeconds, Spawn, true);
		_spawningTimer.StartCounting();
	}

	public void Spawn()
	{
		IBasePoolObject basePoolObject = _poolManager.GetPoolObject(PoolObjectTag, this.transform.position, this.transform.rotation);
        basePoolObject.OnDeactivation += DeactiveSpawnedObject;
		_currentSpawnedObjectNumber++;
		_activeSpawnedObjects.Add(basePoolObject);
		OnSpawn(basePoolObject);
		HandleSpawnEnd();
	}

    public void Reset()
	{
		_spawningTimer.EndCounting();
		DeactivateSpawnedObjects();
	}

	private void DeactivateSpawnedObjects()
    {
        for (int i = _activeSpawnedObjects.Count - 1; i >= 0; i--)
        {
			_activeSpawnedObjects[i].Deactivation();
		}
    }

	private void HandleSpawnEnd()
	{
		if (CheckSpawnEnd() == true)
		{
			_spawningTimer.EndCounting();
			OnSpawnEnd();
		}
	}

	private bool CheckSpawnEnd()
	{
		return _currentSpawnedObjectNumber == SpawnObjectsLimit;
	}

	private void DeactiveSpawnedObject(BasePoolObject obj)
	{
		obj.OnDeactivation -= DeactiveSpawnedObject;
		_activeSpawnedObjects.Remove(obj);
	}

	#endregion

	#region ENUMS

	#endregion
}
