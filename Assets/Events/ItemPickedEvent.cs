using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    [CreateAssetMenu(fileName = nameof(ItemPickedEvent), menuName = "Runner/" + nameof(ItemPickedEvent))]
    public class ItemPickedEvent : AbstractGameEvent
    {
        [HideInInspector]
        public int Count = -1;

        public override void Reset()
        {
            Count = -1;
        }
    }

}
