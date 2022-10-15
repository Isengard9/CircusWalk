using UnityEngine;

namespace Game.General.Managers.Controllers.Player
{
    public class PlayerMovementController : Controller
    {
        [SerializeField]
        private float MovementSpeed = 1;

        [SerializeField] 
        private CharacterController characterController;
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }

            characterController.Move(this.transform.forward * MovementSpeed* Time.deltaTime);
        }

        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            
        }
    }
}