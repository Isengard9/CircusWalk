using UnityEngine;

namespace Game.General.Managers
{
    public class SoundManager : Manager
    {
        [SerializeField]
        private AudioSource audioSource;

        private float value = 1;
        
        public void PlayEffect(bool perfect)
        {
            var pitchValue = perfect ? value += 0.5f : value = 1.0f;
            audioSource.pitch = pitchValue;
            audioSource.Play();
        }
        
        #region On Game Started Ended Paused

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