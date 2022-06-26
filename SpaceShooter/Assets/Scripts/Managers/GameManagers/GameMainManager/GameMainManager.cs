using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Zenject;

namespace Managers.GameManagers
{
	public class GameMainManager : MonoBehaviour, IGameMainManager
	{
		#region FIELDS

		public event Action OnGameStart = delegate { };
		public event Action OnMainOpen = delegate { };
		public event Action OnGameOver = delegate { };
		public event Action OnWaitingOpen = delegate { };

		private IPlayerManager playerManager;
		private IPoolManager poolManager;
		private IInputManager inputManager;
		private IUpdateManager updateManager;
		private IKeyboardManager keyboardManager;
		private SpawnableObjectsTagsEnum tagManager;

		#endregion

		#region PROPERTIES

		public GameState State {
			get;
			set;
		} = GameState.MENU;

		private int LevelNumber {
			get;
			set;
		} = 1;

		#endregion

		#region UNITY_METHODS

		protected virtual void Awake()
		{
			ManagersInitialize();
			AttachInternalEvents();
			OpenMenu();
		}

		protected virtual void OnDestroy()
		{
			DetachInGameEvents();
		}

		#endregion

		#region METHODS

		[Inject]
		public void InjectDependencies(IUpdateManager updateManager, IKeyboardManager keyboardManager, IPlayerManager playerManager, IPoolManager poolManager, IInputManager inputManager)
		{
			this.updateManager = updateManager;
			this.keyboardManager = keyboardManager;
            this.playerManager = playerManager;
            this.poolManager = poolManager;
            this.inputManager = inputManager;
		}

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
			updateManager.PauseTime();
			OnGameOver();
		}

		public void OpenMenu()
		{
			updateManager.UnPauseTime();
			SceneManager.LoadScene(Constants.MainSceneName);
			SetGameState(GameState.MENU);
			DetachInGameEvents();
			OnMainOpen();
		}

		public void OpenWaitingRoom()
		{
			updateManager.UnPauseTime();
			SceneManager.LoadScene(Constants.WaitingRoom);
			SetGameState(GameState.WAITING_ROOM);
			DetachInGameEvents();
			OnWaitingOpen();
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
			if (IsFirstLevel() == true)
			{
				playerManager.ReloadPlayer();
			}
			else
			{
				playerManager.PlayerShootingController.ResetShooting();
			}

			LevelManager.Instance.StartLevel();
		}

		private void ManagersInitialize()
		{
			keyboardManager.Initialize();
			playerManager.Initialize();
			poolManager.Initialize();
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

}
