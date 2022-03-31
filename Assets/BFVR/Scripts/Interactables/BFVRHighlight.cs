using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    public class BFVRHighlight : MonoBehaviour
    {
        Renderer hightlightRenderer;

        void Start()
        {
            hightlightRenderer = gameObject.GetComponent<Renderer>();
            HideHighlight();
        }

        public void HideAllHighlights()
        {
            foreach (BFVRHighlight h in FindObjectsOfType<BFVRHighlight>())
            {
                h.HideHighlight();
            }
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
