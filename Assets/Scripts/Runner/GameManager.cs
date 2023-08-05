using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NarvalDev.Core;
using System;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NarvalDev.Runner
{
    /// <summary>
    /// A class used to store game state information,
    /// load levels, and save/load statistics as applicable
    /// The GameManager class manages all game-related
    /// state changes
    /// </summary>
    public class GameManager : AbstractSingleton<GameManager>
    {
        /// <summary>
        /// Returns the GameManager
        /// </summary>
        public static GameManager s_Instance;

        [SerializeField]
        AbstractGameEvent m_WinEvent;

        [SerializeField]
        AbstractGameEvent m_LoseEvent;

        [SerializeField]
        AbstractGameEvent m_GameStartedEvent;

        [SerializeField]
        int coinCount = 0;


        /// <summary>
        /// Returns true if the game is currently active.
        /// Returns false if the game is paused, has not yet begun,
        /// or has ended
        /// </summary>
        public bool IsPlaying => m_IsPlaying;
        bool m_IsPlaying;

#if UNITY_EDITOR
        bool m_LevelEditorMode;
#endif
        void Awake()
        {
            if(s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;
        }
        
        public void ResetLevel()
        {
            if(Player.Instance != null)
            {
                Player.Instance.ResetPlayer();
            }
        }

        public void StartGame()
        {
            ResetLevel();
            m_IsPlaying = true;
        }
        
        public void Win()
        {
            m_WinEvent.Raise();
#if UNITY_EDITOR
            if(m_LevelEditorMode )
            {
                ResetLevel();
            }
#endif
        }
        public void Lose()
        {
            m_LoseEvent.Raise();
#if UNITY_EDITOR
            if (m_LevelEditorMode)
            {
                ResetLevel();
            }
#endif
        }

        
    }

}
