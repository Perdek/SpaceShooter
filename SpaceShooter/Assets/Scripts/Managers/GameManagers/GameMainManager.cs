using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.PlayerLoop;

public class GameMainManager : BaseMonoBehaviourSingletonManager<GameMainManager>
{

	#region FIELDS

	public event Action OnGameStart = delegate { };
	public event Action OnMainOpen = delegate { };
	public event Action OnGameOver = delegate { };

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

	public ScenesNamesManager ScenesNamesManager {
		get;
		private set;
	} = new ScenesNamesManager();

	public GameState State {
		get;
		private set;
	} = GameState.MENU;

	#endregion

	#region METHODS

	public void SetGameState(GameState newState)
	{
		State = newState;
	}

	public void StartGame()
	{
		SceneManager.LoadScene(ScenesNamesManager.Level01);
		SetGameState(GameState.GAME);
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
		SceneManager.LoadScene(ScenesNamesManager.MainSceneName);
		SetGameState(GameState.MENU);
		OnMainOpen();
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
		if (scene.name != ScenesNamesManager.MainSceneName)
		{
			StartLevel();
		}
	}

	private void StartLevel()
	{
		if (LevelManager.Instance != null)
		{
			LevelManager.Instance.StartLevel();
		}

		if (UISceneManager.Instance != null)
		{
			UISceneManager.Instance.Initialize();
			UISceneManager.Instance.RefreshUI();
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
		ScenesNamesManager.SingletonInitialization();
	}

	private void ManagersInitialize()
	{
		UpdateManager.Initialize();
		InputManager.Initialize();
		KeyboardManager.Initialize();
		PlayerManager.Initialize();
		PoolManager.Initialize();
		TagManager.Initialize();
		ScenesNamesManager.Initialize();
	}

	#endregion

	#region ENUMS

	public enum GameState
	{
		MENU,
		GAME,
		PAUSE
	}

	#endregion
}
