using System;

namespace Managers.GameManagers
{
    public interface IGameMainManager
    {
        #region MEMBERS

        public event Action OnGameStart;
        public event Action OnMainOpen;
        public event Action OnGameOver;
        public event Action OnWaitingOpen;

        #endregion

        #region PROPERTIES

        public GameMainManager.GameState State { get; }

        #endregion

        #region UNITY_METHODS

        #endregion

        #region METHODS

        public void SetGameState(GameMainManager.GameState newState);
        public void StartGame();
        public void StartNextLevel();
        public void GameOver();
        public void OpenMenu();
        public void OpenWaitingRoom();

        #endregion
    }
}
