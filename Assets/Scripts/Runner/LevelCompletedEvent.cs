using NarvalDev.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Gameplay
{
    [CreateAssetMenu(fileName = nameof(LevelCompletedEvent),menuName ="Runner/"+nameof(LevelCompletedEvent))]
    public class LevelCompletedEvent : AbstractGameEvent
    {
        public override void Reset()
        {
        }
    }

}
