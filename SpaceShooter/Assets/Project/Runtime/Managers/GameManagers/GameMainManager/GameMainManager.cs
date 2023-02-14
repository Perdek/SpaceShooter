using UnityEngine;
using System;
using Managers.LevelManagers;
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

		private IPlayerManager _playerManager;
		private IUpdateManager _updateManager;
		private IKeyboardManager _keyboardManager;
		private LevelEventsCommunicator _levelEventsCommunicator;
		private SpawnableObjectsTagsEnum _tagManager;
		
		private GameState _state = GameState.MENU;
		private int _levelNumber = 1;

		#endregion

		#region PROPERTIES

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
		public void InjectDependencies(IUpdateManager updateManager, IKeyboardManager keyboardManager, IPlayerManager playerManager, LevelEventsCommunicator levelEventsCommunicator)
		{
			_updateManager = updateManager;
			_keyboardManager = keyboardManager;
            _playerManager = playerManager;
            _levelEventsCommunicator = levelEventsCommunicator;
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
			_updateManager.PauseTime();
			OnGameOver();
		}

		public void OpenMenu()
		{
			_updateManager.UnPauseTime();
			SceneManager.LoadScene(Constants.MainSceneName);
			_state = GameState.MENU;
			DetachInGameEvents();
			OnMainOpen();
		}

		public void OpenWaitingRoom()
		{
			_updateManager.UnPauseTime();
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
			_updateManager.OnUpdateInputInformation += _keyboardManager.CheckKeys;
		}

		private void DetachInGameEvents()
		{
			SceneManager.sceneLoaded -= OnSceneLoaded;
			_updateManager.OnUpdateInputInformation -= _keyboardManager.CheckKeys;
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
				_playerManager.ReloadPlayer();
			}
			else
			{
				_playerManager.PlayerShootingController.ResetShooting();
			}

			_levelEventsCommunicator.NotifyOnRequestLevelStart();
		}

		private void ManagersInitialize()
		{
			_playerManager.Initialize();
		}

		private void IncreaseLevelNumber()
		{
			_levelNumber++;
		}

		private bool IsFirstLevel()
		{
			return _levelNumber == 1;
		}

		private void ResetLevelNumber()
		{
			_levelNumber = 1;
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
