using System;
using Game.General.Managers;
using General.Controllers.Cube;
using UnityEngine;

namespace General.Controllers.Player
{
    public class PlayerController : Controller
    {
        public Action<CubePartController> onCubeChanged;
        
        #region Variables

        private CubePartController currentCube;

        #endregion
        
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }
            
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
                ManagerContainer.Instance.GameManager.GameEnded?.Invoke(true);
            }
            
        }
    }
}