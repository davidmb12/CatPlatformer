using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    [CreateAssetMenu(fileName =nameof(SceneRef),menuName ="Runner/"+ nameof(SceneRef))]
    public class SceneRef : AbstractLevelData
    {
        [SerializeField]
        public string m_ScenePath;
    }

}
