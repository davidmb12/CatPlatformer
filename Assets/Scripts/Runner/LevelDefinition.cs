using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Runner
{

    /// <summary>
    /// A scriptable object that stores all information
    /// needed to load and set up a Runner level
    /// </summary>
    [CreateAssetMenu(fileName ="Data",menuName ="Runner/LevelDefinition",order =1)]
    public class LevelDefinition : AbstractLevelData
    {
        /// <summary>
        /// The length of the level
        /// </summary>
        public float LevelLength = 100.0f;

        /// <summary>
        /// The amount of extra terrain to be added before the start of the level
        /// </summary>
        public float LevelLengthBufferStart = 5.0f;

        /// <summary>
        /// The amount of extra terrain to be added after the end of the level
        /// </summary>
        public float LevelLengthBufferEnd = 5.0f;

        public SpawnableObject[] Spawnables;

        [System.Serializable]
        public class SpawnableObject
        {
            /// <summary>
            /// The prefab spawned by this SpawnableObject
            /// </summary>
            public GameObject SpawnablePrefab;

            /// <summary>
            /// The world position of this SpawnableObject
            /// </summary>
            public Vector3 Position = Vector3.zero;

            /// <summary>
            /// The rotational euler angles of this SpawnableObject
            /// </summary>
            public Vector3 EulerAngles = Vector3.zero;

            /// <summary>
            /// The world scale of this SpawnableObject
            /// </summary>
            public Vector3 Scale = Vector3.one;

            /// <summary>
            /// The base color to be applied to the materials on
            /// this SpawnableObject
            /// </summary>
            public Color BaseColor = Color.white;

            /// <summary>
            /// True if this object should snap to a levels grid.
            /// Setting this value to false will make this SpawnableObject
            /// ignore the level's snap settings
            /// </summary>
            public bool SnapToGrid = true;

           

        }
        public void SaveValues(LevelDefinition updatedLevel)
        {
            //TODO - Add Tests for this!
            LevelLength = updatedLevel.LevelLength;

        }
    }


}
