using System.Collections.Generic;
using Controllers;
using Game.General.Managers;
using UnityEngine;

namespace General.Controllers.Cube
{
    public class CubeController : Controller
    {
        [Header("Cube")]
        [SerializeField] 
        private GameObject cubePrefab;

        [SerializeField] 
        private GameObject oddCubePrefab;

        [SerializeField]
        private CubePartController lastCube;
        private CubePartController currentCube;
        [SerializeField]
        private GameObject cubeParent;

        [Header("Helper")]
        [SerializeField]
        private List<Color> colorList = new List<Color>();

        [SerializeField]
        private Transform leftPosition, rightPosition;

        private OddCube oddCube;
        private float lastCubeSize = 5;
        public static float AvarageDistance = 0.1f;

        private byte currentCubeCreateIndex = 0;

        protected override void OnStart()
        {
            base.OnStart();
            
        }


        protected override void OnUpdate()
        {
            base.OnUpdate();

            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                StopCube();
            }
        }

        protected override void OnGameStarted()
        {
            base.OnGameStarted();
            CreateCube();
        }
        


        #region Cube Functions

        private void CreateCube()
        {
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
            {
                return;
            }

            if (ManagerContainer.Instance.LevelManager.CurrentLevelCubeCount == currentCubeCreateIndex)
            {
                return;
            }

            var cube = Instantiate(cubePrefab, cubeParent.transform);
            cube.transform.position = Random.value > 0.5f ? leftPosition.position : rightPosition.position;
            lastCubeSize = lastCube.transform.localScale.x;
            cube.transform.localScale = (Vector3.right * lastCubeSize) + (Vector3.up * 1f) + (Vector3.forward * 5);

            var cubePartScript = cube.GetComponent<CubePartController>();
            cubePartScript.SetColor(colorList[Random.Range(0, colorList.Count - 1)]);
            cubePartScript.StartMove();

            currentCube = cubePartScript;

            currentCubeCreateIndex++;
        }

        private void StopCube()
        {
            currentCube?.StopMove();

            oddCube = currentCube?.SplitAction(new OddCube(), lastCube);
            CreateOddCube();
            this.transform.position += Vector3.forward * 5;
            lastCube = currentCube;
            CreateCube();
        }

        private void CreateOddCube()
        {
            if (!oddCube.Create)
            {
                return;
            }

            var cube = Instantiate(oddCubePrefab);
            cube.transform.position = oddCube.Position;
            cube.transform.localScale = oddCube.Scale;
            cube.GetComponent<MeshRenderer>().material.color = oddCube.Color;
            Destroy(cube, 1);
        }

        public void SetLastCube(CubePartController lastCube)
        {
            this.lastCube = lastCube;
            this.transform.position += Vector3.forward * 10;
            
        }

        public void DestroyOldCubes()
        {
            ControllerContainer.Instance.LevelController.levelObjects.Remove(cubeParent);
            Destroy(cubeParent.gameObject);
            cubeParent = new GameObject();
            cubeParent.name = "CubeParent";
            
            ControllerContainer.Instance.LevelController.levelObjects.Add(cubeParent);
        }

        private void CloseCubes()
        {
            var cubes = GameObject.FindObjectsOfType<CubePartController>();

            foreach (var cube in cubes)
            {
                cube.GetComponent<Collider>().enabled = false;
            }
        }

        #endregion

        #region Init DeInit

        protected override void OnInit()
        {
            base.OnInit();
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction += OnLevelCompleted;
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            currentCubeCreateIndex = 1;
        }

        #endregion


        protected override void OnGameEnded(bool win)
        {
            base.OnGameEnded(win);
            CloseCubes();
        }

        protected override void OnGamePaused(bool obj)
        {
            base.OnGamePaused(obj);
            if (!obj)
            {
                CreateCube();
            }
        }
    }
}