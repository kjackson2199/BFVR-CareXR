using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    public class BFVRHighlight : MonoBehaviour
    {
        Outline hightlightRenderer;

        void Start()
        {
            hightlightRenderer = gameObject.GetComponent<Outline>();
        }

        public void ShowHightlight()
        {
            if (hightlightRenderer) hightlightRenderer.enabled = true;
        }

        public void HideHighlight()
        {
            if (hightlightRenderer) hightlightRenderer.enabled = false;
        }
    }
}
