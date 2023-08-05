using NarvalDev.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Runner
{
    public class MainMenu : View
    {
        [SerializeField]
        HyperCasualButton m_StartButton;
        [SerializeField]
        HyperCasualButton m_SettingsButton;
        [SerializeField]
        AbstractGameEvent m_StartButtonEvent;

        public float m_SecondsToStart;

        void OnEnable()
        {
            m_StartButton.AddListener(OnStartButtonClick);
            m_SettingsButton.AddListener(OnSettingsButtonClick);
            
        }
        void OnDisable()
        {
            m_StartButton.RemoveListener(OnStartButtonClick);
            m_SettingsButton.RemoveListener(OnSettingsButtonClick);
        }

        void OnSettingsButtonClick()
        {
            UIManager.Instance.Show<SettingsMenu>();
            AudioManager.Instance.PlayEffect(SoundID.ButtonSound);
        }

        private void OnStartButtonClick()
        {
            Debug.Log(m_StartButtonEvent.name);
            StartCoroutine(WaitForSeconds(m_SecondsToStart));
            m_StartButtonEvent.Raise();
            AudioManager.Instance.PlayEffect(SoundID.ButtonSound);
        }

        IEnumerator WaitForSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
        }

        
    }

}
