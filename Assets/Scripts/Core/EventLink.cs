using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    /// <summary>
    /// A link that listens for a specific event and becomes open for transition if the event is raised
    /// If the current state is linked to the next step by this link type, 
    /// The state machine waits for th event to be triggered and then moves to the next step
    /// </summary>
    public class EventLink : ILink,IGameEventListener
    {
        IState m_NextState;
        AbstractGameEvent m_GameEvent;
        bool m_EventRaised;

        /// <param name="gameEvent">The event this link listens to</param>
        /// <param name="nextState">The next state</param>
        public EventLink(AbstractGameEvent gameEvent,IState nextState)
        {
            m_GameEvent = gameEvent;
            m_NextState = nextState;
        }
        public void OnEventRaised()
        {
            m_EventRaised = true;
        }

        public bool Validate(out IState nextState)
        {
            nextState = null;
            bool result = false;

            if(m_EventRaised)
            {
                nextState = m_NextState;
                result = true;
            }

            return result;
        }

        public void Enable()
        {
            m_GameEvent.AddListener(this);
            m_EventRaised = false;
        }

        public void Disable()
        {
            m_GameEvent.RemoveListener(this);
            m_EventRaised = false;
        }

        
    }

}
