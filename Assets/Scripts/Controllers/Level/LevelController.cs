using System.Collections.Generic;
using Game.General.Managers;
using General.Controllers;
using General.Controllers.Cube;
using UnityEngine;

namespace Controllers.Level
{
    public class LevelController : Controller
    {
        public List<GameObject> levelObjects = new List<GameObject>();


        protected override void OnInit()
        {
            base.OnInit();
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction += OnLevelStartedAction;
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction += OnLevelCompletedAction;
            ControllerContainer.Instance.PlayerController.LevelLastMoveAction += ReadyToNextLevel;
        }

        protected override void OnDeInit()
        {
            base.OnDeInit();
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction -= OnLevelStartedAction;
            ManagerContainer.Instance.LevelManager.OnLevelCompletedAction -= OnLevelCompletedAction;
            ControllerContainer.Instance.PlayerController.LevelLastMoveAction -= ReadyToNextLevel;
        }

        private void OnLevelCompletedAction()
        {
            levelObjects.Add(ManagerContainer.Instance.LevelManager.CurrentFinish);
            ManagerContainer.Instance.LevelManager.CurrentFinish.GetComponent<Collider>().enabled = false;

        }

        private void ReadyToNextLevel()
        {
            
            var position = ControllerContainer.Instance.PlayerController.transform.position;
            position.x = 0;
            position.y = 0;
            this.transform.position = position;
            foreach (var levelObject in levelObjects)
            {
                levelObject.transform.parent = this.transform;
            }

            position.z = 0;


            this.transform.position = position;

            foreach (var levelObject in levelObjects)
            {
                levelObject.transform.parent = null;
            }


            var startCube = levelObjects.Find(x => x.name.Equals("StartCube"));
            startCube.transform.position = Vector3.forward * 5;

            ControllerContainer.Instance.CubeController.SetLastCube(startCube.GetComponent<CubePartController>());

            Invoke("StartNewLevel", 3);
        }

        private void StartNewLevel()
        {
            ManagerContainer.Instance.LevelManager.OnLevelStartedAction?.Invoke();
        }

        private void OnLevelStartedAction()
        {
            ControllerContainer.Instance.CubeController.DestroyOldCubes();
            var finish = levelObjects.Find(x => x.name.Contains("Finish"));
            levelObjects.Remove(finish);
            Destroy(finish, 1);
        }
    }
}