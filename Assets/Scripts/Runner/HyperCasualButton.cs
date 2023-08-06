using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace NarvalDev.Runner
{
    [RequireComponent(typeof(Button))]
    public class HyperCasualButton : MonoBehaviour
    {
        [SerializeField]
        protected Button m_Button;
        [SerializeField]
        SoundID m_ButtonSound = SoundID.ButtonSound;

        public float waitTime;
        Action m_Action;
        
        protected virtual void OnEnable()
        {
            m_Button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            m_Button.onClick.RemoveListener(OnClick);
        }


        /// <summary>
        /// Adds a listener the button event
        /// </summary>
        /// <param name="handler">callback function</param>
        public void AddListener(Action handler)
        {
            m_Action += handler;
        }

        /// <summary>
        /// Removes a listener from the button event
        /// </summary>
        /// <param name="handler">callback function</param>
        public void RemoveListener(Action handler)
        {
            m_Action -= handler;
        }

        protected virtual void OnClick()
        {
            Debug.Log(m_Action.Method.Name);
            PlayButtonSound();
            StartCoroutine(WaitAndAct(waitTime, m_Action));
        }

        IEnumerator WaitAndAct(float seconds, Action action)
        {
            yield return new WaitForSeconds(seconds);
            action?.Invoke();
        }
        protected void PlayButtonSound()
        {
            AudioManager.Instance.PlayEffect(m_ButtonSound);
        }
        
    }

}
