using System;
using System.Collections;
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

    public void StartLevel()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].StartSpawn();
            AddEnemyCount(SpawnPoints[i].SpawnObjectsLimit);
        }

        OnLevelStart();
    }

    public void EndLevel()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].OnSpawn -= HandleEnemySpawn;
            SpawnPoints[i].OnSpawnEnd -= UpdateFinishedSpawnPoints;
            SpawnPoints[i].EndLevel();
        }

        SpawnPoints.Clear();

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

    private void HandleEnemyDeactivation(BasePoolObject destroyedEnemy)
    {
        destroyedEnemy.OnDeactivation -= HandleEnemyDeactivation;
        DescreseEnemyCount();
        HandleCheckLevelEnd();
    }

    private void HandleCheckLevelEnd()
    {
        if (FinishedSpawnPoints == SpawnPoints.Count && EnemyCount == 0)
        {
            EndLevel();
        }
    }

    private void DescreseEnemyCount()
    {
        EnemyCount--;
    }

    private void AddEnemyCount(int amount)
    {
        EnemyCount += amount;
    }

    #endregion

    #region ENUMS

    #endregion
}
