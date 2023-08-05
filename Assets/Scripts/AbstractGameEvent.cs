using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public abstract class AbstractGameEvent : ScriptableObject
    {
        readonly List<IGameEventListener> m_EventListeners = new();

        public void Raise()
        {
            for (int i = m_EventListeners.Count - 1; i >= 0; i--)
            {
                m_EventListeners[i].OnEventRaised();
            }
            Reset();
        }

        public void AddListener(IGameEventListener listener)
        {
            if (!m_EventListeners.Contains(listener))
            {
                m_EventListeners.Add(listener);
            }
        }

        public void RemoveListener(IGameEventListener listener)
        {
            if (m_EventListeners.Contains(listener))
            {
                m_EventListeners.Remove(listener);
            }
        }

        public abstract void Reset();
    }

}
