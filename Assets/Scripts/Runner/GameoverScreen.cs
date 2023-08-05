using NarvalDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class GameoverScreen : View
    {
        [SerializeField]
        HyperCasualButton m_PlayAgainButton;
        [SerializeField]
        HyperCasualButton m_GoToMainMenuButton;
        [SerializeField]
        AbstractGameEvent m_PlayAgainEvent;
        [SerializeField]
        AbstractGameEvent m_GoToMainMenuEvent;

        void OnEnable()
        {
            m_PlayAgainButton.AddListener(OnPlayAgainButtonClick);
            m_GoToMainMenuButton.AddListener(OnGoToMainMenuButtonClick);

        }

        void OnDisable()
        {
            m_PlayAgainButton.RemoveListener(OnPlayAgainButtonClick);
            m_GoToMainMenuButton.RemoveListener(OnGoToMainMenuButtonClick);
        }

        private void OnGoToMainMenuButtonClick()
        {
            m_GoToMainMenuEvent.Raise();
        }

        private void OnPlayAgainButtonClick()
        {
            m_PlayAgainEvent.Raise();

        }
    }

}

