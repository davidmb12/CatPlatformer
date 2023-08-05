using NarvalDev.Core;
using NarvalDev.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NarvalDev.Gameplay
{
    public class LevelSelectionScreen : View
    {
        [SerializeField]
        HyperCasualButton m_QuickPlayButton;
        [SerializeField]
        HyperCasualButton m_BackButton;
        [Space]
        [SerializeField]
        RectTransform m_LevelButtonsRoot;
        [SerializeField]
        AbstractGameEvent m_NextLevelEvent;
        [SerializeField]
        AbstractGameEvent m_BackEvent;
#if UNITY_EDITOR
        [SerializeField]
        bool m_UnlockAllLevels;
#endif


        void Start()
        {
            var levels = SequenceManager.Instance.Levels;
            

            ResetButtonData();
        }

        void OnEnable()
        {
            ResetButtonData();

            //m_QuickPlayButton.AddListener(OnQuickPlayButtonClicked);
            m_BackButton.AddListener(OnBackButtonClicked);
        }

        void OnDisable()
        {
            //m_QuickPlayButton.RemoveListener(OnQuickPlayButtonClicked);
            m_BackButton.RemoveListener(OnBackButtonClicked);
        }

        void ResetButtonData()
        {
            var levelProgress = SaveManager.Instance.LevelProgress;
            
        }

        void OnClick(int startingIndex)
        {
            if (startingIndex < 0)
                throw new Exception("Button is not initialized");

            SequenceManager.Instance.SetStartingLevel(startingIndex);
            m_NextLevelEvent.Raise();
        }

        void OnQuickPlayButtonClicked()
        {
            OnClick(SaveManager.Instance.LevelProgress);
        }

        void OnBackButtonClicked()
        {
            m_BackEvent.Raise();
        }
    }

}
