using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public class Link : ILink
    {
        readonly IState m_NextState;

        public Link(IState nextState)
        {
            m_NextState = nextState;
        }
        public bool Validate(out IState nextState)
        {
            nextState = m_NextState;
            return true;
        }

        
    }

}
