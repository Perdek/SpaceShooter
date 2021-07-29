using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : BaseMonoBehaviourSingletonManager<LevelManager>
{
    #region FIELDS

    public event Action OnLevelStart = delegate { };
    public event Action OnLevelEnd = delegate { };

    [SerializeField]
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

    #endregion

    #region PROPERTIES

    public List<SpawnPoint> SpawnPoints => spawnPoints;

    public LevelState State {
        get;
        private set;
    }

    private int FinishedSpawnPoints {
        get;
        set;
    } = 0;

    private int EnemyCount {
        get;
        set;
    } = 0;

    #endregion

    #region METHODS

    public override void Initialize()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].OnSpawn += HandleEnemySpawn;
            SpawnPoints[i].OnSpawnEnd += UpdateFinishedSpawnPoints;
        }
    }

    public bool IsEndedLevel()
    {
        return State == LevelState.ENDED;
    }

    public void StartLevel()
    {
        State = LevelState.STARTED;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].StartSpawn();
            AddEnemyCount(SpawnPoints[i].SpawnObjectsLimit);
        }

        OnLevelStart();
    }

    public void EndLevel()
    {
        State = LevelState.ENDED;

        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].OnSpawn -= HandleEnemySpawn;
            SpawnPoints[i].OnSpawnEnd -= UpdateFinishedSpawnPoints;
            SpawnPoints[i].Reset();
        }

        OnLevelEnd();
    }

    protected virtual void Awake()
    {
        SingletonInitialization();
        Initialize();
    }

    private void UpdateFinishedSpawnPoints()
    {
        FinishedSpawnPoints++;
        HandleCheckLevelEnd();
    }

    private void HandleEnemySpawn(BasePoolObject spawnedEnemy)
    {
        spawnedEnemy.OnDeactivation += HandleEnemyDeactivation;
    }

    private void HandleEnemyDeactivationDetachEvents(BasePoolObject destroyedEnemy)
    {
        destroyedEnemy.OnDeactivation -= HandleEnemyDeactivation;
    }

    private void HandleEnemyDeactivation(BasePoolObject destroyedEnemy)
    {
        destroyedEnemy.OnDeactivation -= HandleEnemyDeactivation;
        DecreaseEnemyCount();
        HandleCheckLevelEnd();
    }

    private void HandleCheckLevelEnd()
	{
		if (IsAnyMoreEnemies() == true)
		{
			EndLevel();
		}
	}

	private bool IsAnyMoreEnemies()
	{
		return FinishedSpawnPoints == SpawnPoints.Count && EnemyCount == 0;
	}

	private void DecreaseEnemyCount()
    {
        EnemyCount--;
    }

    private void AddEnemyCount(int amount)
    {
        EnemyCount += amount;
    }

    #endregion

    #region ENUMS

    public enum LevelState
    {
        STARTED,
        ENDED
    }

    #endregion
}
