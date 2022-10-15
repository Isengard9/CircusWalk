using System;
using UnityEngine;

namespace Game.General.Managers
{
    public class ManagerContainer : MonoBehaviour
    {
        private static ManagerContainer instance;
        public static ManagerContainer Instance => instance;

        [SerializeField]
        private UIManager uiManager;
        public UIManager UIManager => uiManager;

        [SerializeField]
        private GameManager gameManager;
        public GameManager GameManager => gameManager;


        [SerializeField]
        private CameraManager cameraManager;
        public CameraManager CameraManager => cameraManager;
        
        private void Awake()
        {
            instance = this;
        }

        private void OnDestroy()
        {
            
        }
    }
}