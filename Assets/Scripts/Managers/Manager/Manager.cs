using System;
using UnityEngine;

namespace Game.General.Managers
{
    public abstract class Manager : MonoBehaviour, IManager
    {
        #region UnityFunctions
        
        protected virtual void OnAwake()
        {
            
        }
        public void Awake()
        {
            OnAwake();
        }

        protected virtual void OnStart()
        {
            
        }
        private void Start()
        {
            Init();
            OnStart();
        }

        private void OnDestroy()
        {
            DeInit();
        }

        protected virtual void OnUpdate()
        {
            
        }
        private void Update()
        {
            OnUpdate();
        }

        #endregion

        #region Init / DeInit

        protected virtual void Init()
        {
            ManagerContainer.Instance.GameManager.GameStarted += OnGameStarted;
            ManagerContainer.Instance.GameManager.GameEnded += OnGameEnded;
            ManagerContainer.Instance.GameManager.GamePaused += OnGamePaused;
        }
        
        protected virtual void DeInit()
        {
            ManagerContainer.Instance.GameManager.GameStarted -= OnGameStarted;
            ManagerContainer.Instance.GameManager.GameEnded -= OnGameEnded;
            ManagerContainer.Instance.GameManager.GamePaused -= OnGamePaused;
        }

        protected abstract void OnGameStarted();
        
        protected abstract void OnGamePaused(bool obj);

        protected abstract void OnGameEnded(bool win);

        #endregion
    }
}