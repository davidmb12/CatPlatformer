using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Core
{
    public abstract class View: MonoBehaviour
    {
        /// <summary>
        /// Initializes the view
        /// </summary>
        public virtual void Initialize() 
        {
        }

        /// <summary>
        /// Makes the view visible
        /// </summary>
        public virtual void Show()
        {
            Debug.Log("Show "+this.name);
            gameObject.SetActive(true);
        }

        public virtual void PlayAnimation(string animName)
        {
            gameObject.GetComponent<Animator>().Play(animName);
        }
        /// <summary>
        /// Hides the view
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }


    }
}