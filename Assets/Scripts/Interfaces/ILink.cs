using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public interface ILink
    {
        /// <summary>
        /// The owner state-machine calls this method to examine the link and determines if it's open for transition.
        /// The state-machine will transit to the next state determined by the first open link of the current state.
        /// If all links of the state return false, the state-machine returns in the current state
        /// </summary>
        /// <param name="nextState">The next state that this link points to</param>
        /// <returns>true: the link is open for transition</returns>
        bool Validate(out IState nextState);
        
        /// <summary>
        /// Activates the link
        /// </summary>
        void Enable() {}

        /// <summary>
        /// Deactivates the link
        /// </summary>
       void Disable() {}
    }

}
