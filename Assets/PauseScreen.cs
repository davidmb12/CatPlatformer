using NarvalDev.Core;
using NarvalDev.Runner;
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

        void OnEnable()
        {
            m_ResumeButton.AddListener(OnPauseButtonClick);
        }

        void OnDisable()
        {
            m_ResumeButton.RemoveListener(OnPauseButtonClick);
        }

        
        void OnPauseButtonClick()
        {
            m_BackEvent.Raise();
        }

    }

}
