using NarvalDev.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class PauseMenu : View
    {
        [SerializeField]
        HyperCasualButton m_ContinueButton;
        [SerializeField]
        HyperCasualButton m_QuitButton;
        [SerializeField]
        AbstractGameEvent m_ContinueEvent;
        [SerializeField]
        AbstractGameEvent m_QuitEvent;

        void OnEnable()
        {
            m_ContinueButton.AddListener(OnContinueClicked);
            m_QuitButton.AddListener(OnQuitClicked);
        }

        void OnDisable()
        {
            m_ContinueButton.RemoveListener(OnContinueClicked);
            m_QuitButton.RemoveListener(OnQuitClicked);
        }

        void OnContinueClicked()
        {
            m_ContinueEvent.Raise();
        }

        void OnQuitClicked()
        {
            m_QuitEvent.Raise();
        }
    }

}
