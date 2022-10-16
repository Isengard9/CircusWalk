using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Game.General.Managers
{
    public class LevelManager : Manager
    {
        public Action OnLevelCompletedAction;
        public Action OnLevelStartedAction;
        
        [SerializeField] private List<byte> levelCubeCount = new List<byte>();
        public List<byte> LevelCubeCount => levelCubeCount;

        private byte levelIndex = 0;

        public byte CurrentLevelCubeCount => levelCubeCount[levelIndex];
        
        
        [SerializeField]
        private GameObject FinishPrefab;

        private GameObject currentFinish;
        public GameObject CurrentFinish => currentFinish;
        protected override void OnStart()
        {
            base.OnStart();
            InitLevel();
        }

        private void InitLevel()
        {
            currentFinish = Instantiate(FinishPrefab);
            currentFinish.transform.position = (Vector3.forward * (levelCubeCount[levelIndex] + 1) * 5) + (Vector3.up*0.5f);
            currentFinish.GetComponent<Collider>().enabled = true;
        }

        public void OnLevelCompleted()
        {
            currentFinish = null;
        }
        
        private void OnLevelStarted()
        {
            levelIndex++;
            
            if (levelIndex >= levelCubeCount.Count)
            {
                levelIndex = 0;
            }
            
            InitLevel();
        }

        #region Init DeInit

        protected override void Init()
        {
            base.Init();
            OnLevelCompletedAction += OnLevelCompleted;
            OnLevelStartedAction += OnLevelStarted;
        }

        protected override void DeInit()
        {
            base.DeInit();
            OnLevelCompletedAction -= OnLevelCompleted;
            OnLevelStartedAction -= OnLevelStarted;
        }

        #endregion


        #region On Game Started Paused Ended

        protected override void OnGameStarted()
        {
            
        }

        protected override void OnGamePaused(bool obj)
        {
            
        }

        protected override void OnGameEnded(bool win)
        {
            
        }

        #endregion
    }
}