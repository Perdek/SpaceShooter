using Managers.GameManagers;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : BaseMonoBehaviourSingletonManager<LevelManager>
{
    #region FIELDS

    public event Action OnLevelStart = delegate { };
    public event Action OnLevelEnd = delegate { };

    [SerializeField]
    private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    [SerializeField]
    private List<ParallaxGroup> parallaxGroups = new List<ParallaxGroup>();

    private IUpdateManager updateManager;

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

    #region METHODS

    [Inject]
    public void InjectDependencies(IUpdateManager newUpdateManager)
    {
        updateManager = newUpdateManager;
    }

    public override void Initialize()
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            SpawnPoints[i].OnSpawn += HandleEnemySpawn;
            SpawnPoints[i].OnSpawnEnd += UpdateFinishedSpawnPoints;
        }

        for (int i = 0; i < ParallaxGroups.Count; i++)
        {
            updateManager.OnUpdateView += ParallaxGroups[i].UpdateParallaxEffects;
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

        for (int i = 0; i < ParallaxGroups.Count; i++)
        {
            updateManager.OnUpdateView -= ParallaxGroups[i].UpdateParallaxEffects;
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
