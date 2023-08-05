using NarvalDev.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public class Collectable : MonoBehaviour
    {
        [SerializeField]
        SoundID m_Sound = SoundID.None;

        const string k_PlayerTag = "Player";

        public ItemPickedEvent m_Event;
        public int m_Count;

        bool m_Collected;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(k_PlayerTag) && !m_Collected)
            {
                Collect();
            }
        }

        void Collect()
        {
            if (m_Event != null)
            {
                m_Event.Count = m_Count;
                m_Event.Raise();
            }
            m_Collected = true;
            AudioManager.Instance.PlayEffect(m_Sound);
            Destroy(gameObject);
        }
    }

}
