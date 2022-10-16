using System;
using Game.General.Managers;
using General.Controllers.Cube;
using UnityEngine;

namespace General.Controllers.Player
{
    public class PlayerController : Controller
    {
        public Action<CubePartController> onCubeChanged;
        public Action LevelLastMoveAction;
        #region Variables

        private CubePartController currentCube;

        [SerializeField]
        private Animator playerAnimator;

        #endregion

        #region AnimationFunctions

        private void PlayRun()
        {
            playerAnimator.SetTrigger("Run");
        }

        private void PlayWin()
        {
            playerAnimator.SetTrigger("Win");
        }

        #endregion

        #region Trigger Functions

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }
            ControlCube(hit);
            ControlFinishPoint(hit);
            ControlDestroyPoint(hit);
            
        }

        private void ControlCube(ControllerColliderHit hit)
        {
            if (currentCube is not null && currentCube.transform == hit.transform)
            {
                return;
            }
            
            var cubePartController = hit.transform.GetComponent<CubePartController>();
        
            if (cubePartController is not null && cubePartController.IsPlaced)
            {
                onCubeChanged?.Invoke(cubePartController);
                currentCube = cubePartController;
            }
            
            if (cubePartController is not null && !cubePartController.IsPlaced)
            {
                GameManager.EndGame(false);
            }
        }

        private void ControlFinishPoint(ControllerColliderHit hit)
        {
            var finishPoint = hit.transform.name.Contains("Finish") ? true : false;

            if (finishPoint)
            {
                ManagerContainer.Instance.LevelManager.OnLevelCompletedAction?.Invoke();
                currentCube = null;
            }
            
        }

        private void ControlDestroyPoint(ControllerColliderHit hit)
        {
            var destroyPoint = hit.transform.name.Contains("DestroyPoint") ? true : false;

            if (destroyPoint)
            {
                GameManager.EndGame(false);
            }
        }

        #endregion
        
        private void OnLevelCompleted()
        {
            PlayWin();
        }

        private void OnLevelStarted()
        {
            PlayRun();
        }

        #region Init DeInit

        protected override void OnInit()
        {
            base.OnInit();
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction += OnLevelStarted;
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction += OnLevelCompleted;
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction -= OnLevelStarted;
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction -= OnLevelCompleted;
        }

        #endregion
        
    }
}