using Managers.GameManagers;
using System;
using System.Collections.Generic;
using Managers.LevelManagers;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    #region FIELDS

    [SerializeField]
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField]
    private List<ParallaxGroup> parallaxGroups = new List<ParallaxGroup>();

    private IUpdateManager _updateManager;
    private LevelEventsCommunicator _levelEventsCommunicator;

    #endregion

    #region PROPERTIES

    public List<SpawnPoint> SpawnPoints => spawnPoints;
    public List<ParallaxGroup> ParallaxGroups => parallaxGroups;

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

    public SpawnPoint SpawnPoint {
        get => default;
        set {
        }
    }

    public ParallaxGroup ParallaxGroup {
        get => default;
        set {
        }
    }

    #endregion
    
    #region UNITY_METHODS

    protected virtual void Awake()
    {
        Initialize();
    }
    
    #endregion

    #region METHODS

    [Inject]
    public void InjectDependencies(IUpdateManager newUpdateManager, LevelEventsCommunicator levelEventsCommunicator)
    {
        _updateManager = newUpdateManager;
        _levelEventsCommunicator = levelEventsCommunicator;
    }

    public void Initialize()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].OnSpawn += HandleEnemySpawn;
            SpawnPoints[i].OnSpawnEnd += UpdateFinishedSpawnPoints;
        }

        for (int i = 0; i < ParallaxGroups.Count; i++)
        {
            _updateManager.OnUpdateView += ParallaxGroups[i].UpdateParallaxEffects;
        }

        _levelEventsCommunicator.IsLevelEnded += IsEndedLevel;
        _levelEventsCommunicator.OnRequestLevelEnd += EndLevel;
        _levelEventsCommunicator.OnRequestLevelStart += StartLevel;
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

        _levelEventsCommunicator.NotifyOnLevelStart();
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

        for (int i = 0; i < ParallaxGroups.Count; i++)
        {
            _updateManager.OnUpdateView -= ParallaxGroups[i].UpdateParallaxEffects;
        }
        
        _levelEventsCommunicator.NotifyOnLevelEnd();
        
        _levelEventsCommunicator.IsLevelEnded -= IsEndedLevel;
        _levelEventsCommunicator.OnRequestLevelEnd -= EndLevel;
        _levelEventsCommunicator.OnRequestLevelStart -= StartLevel;
    }

    private void UpdateFinishedSpawnPoints()
    {
        FinishedSpawnPoints++;
        HandleCheckLevelEnd();
    }

    private void HandleEnemySpawn(IBasePoolObject spawnedEnemy)
    {
        spawnedEnemy.OnDeactivation += HandleEnemyDeactivation;
    }

    private void HandleEnemyDeactivationDetachEvents(IBasePoolObject destroyedEnemy)
    {
        destroyedEnemy.OnDeactivation -= HandleEnemyDeactivation;
    }

    private void HandleEnemyDeactivation(IBasePoolObject destroyedEnemy)
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
