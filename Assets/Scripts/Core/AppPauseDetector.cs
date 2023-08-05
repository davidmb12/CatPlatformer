using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public class AppPauseDetector : MonoBehaviour
    {
        [SerializeField]
        AbstractGameEvent m_PauseEvent;

        /// <summary>
        /// Returns the current pause state of the application
        /// </summary>
        public bool IsPaused { get; private set; }

        private void OnApplicationFocus(bool hasFocus)
        {
            IsPaused = !hasFocus;
            if(IsPaused)
            {
                m_PauseEvent.Raise();
            }
        }

        private void OnApplicationPause(bool pause)
        {
            IsPaused = pause;
            if(IsPaused )
            {
                m_PauseEvent.Raise();
            }
        }
    }

}
