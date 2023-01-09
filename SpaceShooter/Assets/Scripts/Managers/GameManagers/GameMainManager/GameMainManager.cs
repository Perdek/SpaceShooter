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
		private IUpdateManager updateManager;
		private IKeyboardManager keyboardManager;
		private SpawnableObjectsTagsEnum tagManager;
		
		private GameState _state = GameState.MENU;

		#endregion

		#region PROPERTIES

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
		public void InjectDependencies(IUpdateManager updateManager, IKeyboardManager keyboardManager, IPlayerManager playerManager)
		{
			this.updateManager = updateManager;
			this.keyboardManager = keyboardManager;
            this.playerManager = playerManager;
		}

		public void StartGame()
		{
			SceneManager.LoadScene(Constants.Level01);
			_state = GameState.GAME;
			ResetLevelNumber();
			OnGameStart();
		}

		public void StartNextLevel()
		{
			SceneManager.LoadScene(Constants.Level01);
			_state = GameState.GAME;
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
			_state = GameState.MENU;
			DetachInGameEvents();
			OnMainOpen();
		}

		public void OpenWaitingRoom()
		{
			updateManager.UnPauseTime();
			SceneManager.LoadScene(Constants.WaitingRoom);
			_state = GameState.WAITING_ROOM;
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
			updateManager.OnUpdateInputInformation += keyboardManager.CheckKeys;
		}

		private void DetachInGameEvents()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			updateManager.OnUpdateInputInformation -= keyboardManager.CheckKeys;
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
			playerManager.Initialize();
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
