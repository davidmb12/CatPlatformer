using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace NarvalDev.Runner
{
    /// <summary>
    /// A base class for all objects which populate a 
    /// LevelDefinition. This class includes all logic
    /// necessary for snapping an object to a level's grid
    /// </summary>
    [ExecuteInEditMode]
    public class Spawnable : MonoBehaviour
    {
        protected Transform m_Transform;

        LevelDefinition m_LevelDefinition;
        Vector3 m_Position;
        Color m_BaseColor;
        bool m_SnappedThisFrame;
        float m_PreviousGridSize;

        MeshRenderer[] m_MeshRenderers;

        [SerializeField]
        bool m_SnapToGrid = true;

        /// <summary>
        /// The position of this Spawnable, as it is saved.
        /// This value does not factor in any snapping.
        /// </summary>
        public Vector3 SavedPosition => m_Position;

        /// <summary>
        /// The base color to be applied to this Spawnable's
        /// materials.
        /// </summary>
        public Color BaseColor => m_BaseColor;
    
        protected virtual void Awake()
        {
            m_Transform = transform;
            if(m_MeshRenderers == null || m_MeshRenderers.Length == 0)
            {
                m_MeshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            }
            if(m_MeshRenderers != null && m_MeshRenderers.Length > 0)
            {
                m_BaseColor = m_MeshRenderers[0].sharedMaterial.color;
            }
            if(LevelManager.Instance != null)
            {
#if UNITY_EDITOR
                if (PrefabUtility.IsPartOfNonAssetPrefabInstance(gameObject))
#endif
                m_Transform.SetParent(LevelManager.Instance.transform);
            }
        }
    }

}
