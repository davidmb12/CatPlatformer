using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    [Serializable]
    public class GenericGameEventListener : IGameEventListener
    {
        public AbstractGameEvent m_Event;

        /// <summary>
        /// The event handler invoked once the event is triggered
        /// </summary>
        public Action EventHandler;

        public void Subscribe()
        {
            m_Event.AddListener(this);
            
        }

        public void Unsubscribe() 
        {
            m_Event.RemoveListener(this); 
        }


        public void OnEventRaised()
        {
            EventHandler?.Invoke();
        }
    }
}
