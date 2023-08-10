using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NarvalDev.Core
{
    public class PauseState : AbstractState
    {
        readonly Action m_OnPause;

        public override string Name =>$"{nameof(PauseState)}";

        public PauseState(Action onPause)
        {
            m_OnPause = onPause;
        }

        public override void Enter()
        {
            Time.timeScale = 0f;
            m_OnPause?.Invoke();
        }

        public override void Exit()
        {
            Time.timeScale = 1f;
        }
        public override IEnumerator Execute()
        {
            yield return null;
        }

       
    }

}
