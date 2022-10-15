using UnityEngine;

namespace Game.General.Managers.Controllers
{
    public abstract class Controller : MonoBehaviour, IController
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

        protected virtual void OnInit()
        {

        }

        protected virtual void OnDeInit()
        {

        }

        protected virtual void OnGameStarted(){}

        protected virtual void OnGamePaused(bool obj){}

        protected virtual void OnGameEnded(bool win){}

        #endregion

        public void Init()
        {
            ManagerContainer.Instance.GameManager.GameStarted += OnGameStarted;
            ManagerContainer.Instance.GameManager.GameEnded += OnGameEnded;
            ManagerContainer.Instance.GameManager.GamePaused += OnGamePaused;
            OnInit();
        }

        public void DeInit()
        {
            ManagerContainer.Instance.GameManager.GameStarted -= OnGameStarted;
            ManagerContainer.Instance.GameManager.GameEnded -= OnGameEnded;
            ManagerContainer.Instance.GameManager.GamePaused -= OnGamePaused;
            OnDeInit();
        }
    }
}