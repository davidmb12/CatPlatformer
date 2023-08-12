using NarvalDev.Core;
using NarvalDev.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace NarvalDev.Gameplay
{
    public class PauseScreen : View
    {
        [SerializeField]
        HyperCasualButton m_ResumeButton;
        [SerializeField]
        HyperCasualButton m_SettingsButton;
        [SerializeField]
        HyperCasualButton m_QuitButton;

        [SerializeField]
        AbstractGameEvent m_BackEvent;
        [SerializeField]
        AbstractGameEvent m_QuitEvent;

        void OnEnable()
        {
            m_ResumeButton.AddListener(OnPauseButtonClick);
            m_QuitButton.AddListener(OnQuitButtonClick);
        }

        private void OnQuitButtonClick()
        {
            m_QuitEvent.Raise();
        }

        void OnDisable()
        {
            m_ResumeButton.RemoveListener(OnPauseButtonClick);
            m_QuitButton.RemoveListener(OnQuitButtonClick);
        }

        
        void OnPauseButtonClick()
        {
            m_BackEvent.Raise();
        }

    }

}
