using System;

using Game.General.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace Game.General.Managers
{
    public class GameManager : Manager
    {

        #region Actions

        public Action GameStarted;
        public Action<bool> GameEnded;
        public Action<bool> GamePaused;

        #endregion

        #region Booleans

        private static bool isGameStarted = false;
        private static bool isGameEnded = false;
        private static bool isGamePaused = false;

        public bool IsGameStarted => isGameStarted;
        public bool IsGameEnded => IsGameEnded;
        public bool IsGamePaused => IsGamePaused;


        public bool IsGamePlayable => isGameStarted && !isGameEnded && !isGamePaused; 

        #endregion

        protected override void OnStart()
        {
            base.OnStart();
            StartGame();
        }

        #region Game Functions

        protected override void OnGameStarted()
        {
            isGameStarted = true;
            isGameEnded = false;
            isGamePaused = false;
        }

        protected override void OnGameEnded(bool win)
        {
            isGameEnded = true;
        }

        protected override void OnGamePaused(bool state)
        {
            isGamePaused = state;
        }

        #endregion
        
        #region Start Game

        public static void StartGame()
        {
            isGameStarted = true;
            ManagerContainer.Instance.GameManager.GameStarted?.Invoke();
        }

        #endregion

        #region Init DeInit

        protected override void Init()
        {
            base.Init();
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction += OnLevelCompleted ;
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction += OnLevelStarted;
        }

        protected override void DeInit()
        {
            base.DeInit();
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction -= OnLevelCompleted ;
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction -= OnLevelStarted;
        }

        private void OnLevelStarted()
        {
            PauseGame(false);
        }

        private void OnLevelCompleted()
        {
            PauseGame(true);
        }

        #endregion

        #region End Game

        public static void EndGame(bool win)
        {
            ManagerContainer.Instance.GameManager.GameEnded?.Invoke(win);
            
        }

        #endregion

        #region Pause Game

        public static void PauseGame(bool state)
        {
            isGamePaused = state;
            ManagerContainer.Instance.GameManager.GamePaused?.Invoke(state);
        }

        #endregion

        #region Reset Game

        public void ResetGame()
        {
            isGameStarted = isGameEnded = isGamePaused = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        #endregion
    }
}
