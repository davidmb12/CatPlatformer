using NarvalDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class LevelCompleteScreen : View
    {
        [SerializeField]
        HyperCasualButton m_NextButton;
        [SerializeField]
        AbstractGameEvent m_NextLevelEvent;

        void OnEnable()
        {
            m_NextButton.AddListener(OnNextButtonClicked);
        }
        void OnDisable()
        {
            m_NextButton.RemoveListener(OnNextButtonClicked);
        }


        private void OnNextButtonClicked()
        {
            m_NextLevelEvent.Raise();
        }
    }

}
