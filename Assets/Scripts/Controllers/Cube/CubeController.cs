using System.Collections.Generic;
using Game.General.Managers;
using UnityEngine;

namespace General.Controllers.Cube
{
    public class CubeController : Controller
    {
        [Header("Cube")] [SerializeField] private GameObject cubePrefab;

        [SerializeField] private GameObject oddCubePrefab;

        [SerializeField] private CubePartController lastCube;
        private CubePartController currentCube;
        private GameObject cubeParent;

        [Header("Helper")] [SerializeField] private List<Color> colorList = new List<Color>();

        [SerializeField] private Transform leftPosition, rightPosition;

        private OddCube oddCube;
        private float lastCubeSize = 5;
        public static float AvarageDistance = 0.1f;

        protected override void OnStart()
        {
            base.OnStart();
            cubeParent = new GameObject();
            cubeParent.name = "Cube Parent";
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


        private void CreateCube()
        {
            if (!ManagerContainer.Instance.GameManager.IsGamePlayable)
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
        }

        private void StopCube()
        {
            currentCube?.StopMove();

            oddCube = currentCube?.CalculateThePart(new OddCube(), lastCube);
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
    }
}