using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NarvalDev.Runner
{
    /// <summary>
    /// A class used to hold a reference to the current
    /// level and provide access to other classes
    /// </summary>
    [ExecuteInEditMode]
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the LevelManager
        /// </summary>
        public static LevelManager Instance => s_Instance;
        static LevelManager s_Instance;

        /// <summary>
        /// Returns the LevelDefinition used to create this LevelManager
        /// </summary>
        public LevelDefinition LevelDefinition
        {
            get => m_LevelDefinition;
            set
            {
                m_LevelDefinition = value;
                if(m_LevelDefinition != null && Player.Instance != null)
                {
                }
            }
        }

        LevelDefinition m_LevelDefinition;
        List<Spawnable> m_ActiveSpawnables = new List<Spawnable>();


    }

}
