using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NarvalDev.Gameplay
{
    public class BootLoader : MonoBehaviour
    {
        [SerializeField]
        SequenceManager m_SequenceManagerPrefab;
        // Start is called before the first frame update
        void Start()
        {
            Instantiate(m_SequenceManagerPrefab);
            SequenceManager.Instance.Initialize();
        }


    }
}

