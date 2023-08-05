using NarvalDev.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NarvalDev.Gameplay
{
    public class TransitionScreen : View
    {
        [SerializeField]
        GameObject loadingIcon;
        [SerializeField]
        GameObject background;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Show");
        }

        void ActiaveteLoadingIcon()
        {
            loadingIcon.SetActive(true);
        }

        void DeactivateBackground()
        {
            background.SetActive(false);
        }

        void DeactivateLoadingIcon()
        {
            loadingIcon.SetActive(false);

        }


    }

}
