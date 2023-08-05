using NarvalDev.Core;
using NarvalDev.Runner;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NarvalDev.Gameplay
{
    public class Hud : View
    {
        [SerializeField]
        TextMeshProUGUI m_PointsText;
        [SerializeField]
        Slider m_XpSlider;
        [SerializeField]
        HyperCasualButton m_PauseButton;
        [SerializeField]
        HyperCasualButton m_TapStartButton;

        [SerializeField]
        AbstractGameEvent m_PauseEvent;
        [SerializeField]
        AbstractGameEvent m_ContinueGameEvent;
        /// <summary>
        /// The slider that displays the XP value
        /// </summary>
        public Slider XpSlider => m_XpSlider;

        int m_PointsValue;

        public int PointsValue
        {
            get => m_PointsValue;
            set
            {
                if (m_PointsValue != value)
                {
                    m_PointsValue = value;
                    m_PointsText.text = PointsValue.ToString();
                }
            }
        }

        float m_XpValue;

        /// <summary>
        /// The amount of XP to display on the hud
        /// The setter method also sets the hud slider value.
        /// </summary>
        public float XpValue
        {
            get => m_XpValue;
            set
            {
                if (!Mathf.Approximately(m_XpValue, value))
                {
                    m_XpValue = value;
                    m_XpSlider.value = m_XpValue;
                }
            }
        }

        void OnEnable()
        {
            m_PauseButton.AddListener(OnPauseButtonClick);
            m_TapStartButton.AddListener(OnTapStartButtonClick);
        }

        void OnDisable()
        {
            m_PauseButton.RemoveListener(OnPauseButtonClick);
            m_TapStartButton.RemoveListener(OnTapStartButtonClick);
        }

        void OnTapStartButtonClick()
        {
            m_ContinueGameEvent.Raise();
        }
        void OnPauseButtonClick()
        {
            m_PauseEvent.Raise();
        }
        
       
        

    }

}
