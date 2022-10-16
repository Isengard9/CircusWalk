using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.General.Managers
{
    public class UIManager : Manager
    {
        [SerializeField]
        private GameObject LevelFailedObject;
        
        
        #region On Game Started/Paused/Ended

        protected override void OnGameStarted()
        {
        }

        protected override void OnGamePaused(bool obj)
        {
        }

        protected override void OnGameEnded(bool win)
        {
            LevelFailedObject.SetActive(true);
        }

        #endregion

    }
}