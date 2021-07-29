using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameMainManager : BaseMonoBehaviourSingletonManager<GameMainManager>
{

    #region FIELDS

    public event Action OnGameStart = delegate { };
    public event Action OnMainOpen = delegate { };
    public event Action OnGameOver = delegate { };
    public event Action OnWaitingOpen = delegate { };

    [SerializeField]
    private PlayerManager playerManager = null;
    [SerializeField]
    private PoolManager poolManager = null;
    [SerializeField]
    private UpdateManager updateManager = null;
    [SerializeField]
    private InputManager inputManager = null;

    #endregion

    #region PROPERTIES

    public PlayerManager PlayerManager => playerManager;
    public PoolManager PoolManager => poolManager;
    public UpdateManager UpdateManager => updateManager;
    public InputManager InputManager => inputManager;

    public KeyboardManager KeyboardManager {
        get;
        private set;
    } = new KeyboardManager();

    public TagManager TagManager {
        get;
        private set;
    } = new TagManager();

    public GameState State {
        get;
        private set;
    } = GameState.MENU;

    private int LevelNumber {
        get;
        set;
    } = 1;

    #endregion

    #region METHODS

    public void SetGameState(GameState newState)
    {
        State = newState;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Constants.Level01);
        SetGameState(GameState.GAME);
        ResetLevelNumber();
        OnGameStart();
    }

    public void StartNextLevel()
    {
        SceneManager.LoadScene(Constants.Level01);
        SetGameState(GameState.GAME);
        IncreaseLevelNumber();
        OnGameStart();
    }

    public void GameOver()
    {
        UpdateManager.PauseTime();
        OnGameOver();
    }

    public void OpenMenu()
    {
        UpdateManager.UnPauseTime();
        SceneManager.LoadScene(Constants.MainSceneName);
        SetGameState(GameState.MENU);
        DetachInGameEvents();
        OnMainOpen();
    }

    public void OpenWaitingRoom()
    {
        UpdateManager.UnPauseTime();
        SceneManager.LoadScene(Constants.WaitingRoom);
        SetGameState(GameState.WAITING_ROOM);
        DetachInGameEvents();
        OnWaitingOpen();
    }

    protected virtual void Awake()
    {
        SingletonsInitializes();
        ManagersInitialize();
        DontDestroyOnLoad(this);
        AttachInternalEvents();
        OpenMenu();
    }

    protected virtual void OnDestroy()
    {
        DetachInGameEvents();
    }

    private void AttachInternalEvents()
    {
        OnGameStart += AttachInGameEvents;
    }

    private void DetachInternalEvents()
    {
        OnGameStart -= AttachInGameEvents;
    }

    private void AttachInGameEvents()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void DetachInGameEvents()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != Constants.MainSceneName)
        {
            StartLevel();
        }
    }

    private void StartLevel()
    {
        if (PlayerManager.Instance != null)
        {
            if (IsFirstLevel() == true)
            {
                PlayerManager.Instance.ReloadPlayer();
            }
            else
            {
                PlayerManager.Instance.PlayerShootingController.ResetShooting();
            }

        }

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.StartLevel();
        }

        if (UISceneManager.Instance != null)
        {
            UISceneManager.Instance.Initialize();
        }
    }

    private void SingletonsInitializes()
    {
        SingletonInitialization();
        UpdateManager.SingletonInitialization();
        InputManager.SingletonInitialization();
        KeyboardManager.SingletonInitialization();
        PlayerManager.SingletonInitialization();
        PoolManager.SingletonInitialization();
        TagManager.SingletonInitialization();
    }

    private void ManagersInitialize()
    {
        UpdateManager.Initialize();
        InputManager.Initialize();
        KeyboardManager.Initialize();
        PlayerManager.Initialize();
        PoolManager.Initialize();
        TagManager.Initialize();
    }

    private void IncreaseLevelNumber()
    {
        LevelNumber++;
    }

    private bool IsFirstLevel()
    {
        return LevelNumber == 1;
    }

    private void ResetLevelNumber()
    {
        LevelNumber = 1;
    }

    #endregion

    #region ENUMS

    public enum GameState
    {
        MENU,
        GAME,
        PAUSE,
        WAITING_ROOM
    }

    #endregion
}
