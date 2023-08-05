using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public class UnloadLastSceneState : AbstractState
    {
        readonly SceneController m_SceneController;

        public UnloadLastSceneState(SceneController sceneController)
        {
            m_SceneController = sceneController;
        }

        public override IEnumerator Execute()
        {
            yield return m_SceneController.UnloadLastScene();
        }
    }
}