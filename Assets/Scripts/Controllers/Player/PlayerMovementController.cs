using Controllers;
using DG.Tweening;
using Game.General.Managers;
using General.Controllers.Cube;
using UnityEngine;

namespace General.Controllers.Player
{
    public class PlayerMovementController : Controller
    {
        #region Variables

        [SerializeField] private float MovementSpeed = 1;

        [SerializeField] private CharacterController characterController;

        private CubePartController currentCubePart;

        #endregion

        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }

            characterController.Move(this.transform.forward * MovementSpeed * Time.deltaTime);
            
            characterController.Move(Physics.gravity * Time.deltaTime);
        }


        private void MoveCenterOfCube()
        {
            
            var centerPosition = this.transform.position;
            centerPosition.x = currentCubePart.transform.position.x;

            characterController.transform.DOMove(centerPosition, 0.1f);
        }

        protected override void OnInit()
        {
            base.OnInit();
            ControllerContainer.Instance.PlayerController.onCubeChanged += OnCubeChanged;
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            ControllerContainer.Instance.PlayerController.onCubeChanged -= OnCubeChanged;
        }

        private void OnCubeChanged(CubePartController obj)
        {
            currentCubePart = obj;
            MoveCenterOfCube();
        }
    }
}