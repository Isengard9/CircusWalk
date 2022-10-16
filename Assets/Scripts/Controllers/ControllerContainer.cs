using Controllers.Level;
using General.Controllers;
using General.Controllers.Cube;
using General.Controllers.Player;
using UnityEngine;

namespace Controllers
{
    public class ControllerContainer : MonoBehaviour
    {
        private static ControllerContainer instance;
        public static ControllerContainer Instance => instance;

        [SerializeField]
        private PlayerController playerController;
        public PlayerController PlayerController => playerController;

        [SerializeField]
        private CubeController cubeController;
        public CubeController CubeController => cubeController;

        [SerializeField]
        private LevelController levelController;
        public LevelController LevelController => levelController;
        
        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            
        }
    }
}