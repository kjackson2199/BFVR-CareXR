using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.UIModule
{
    public class BFVRTVUIManager : MonoBehaviour
    {
        #region TV Interface Prompt
        public GameObject TVScreenMesh;
        public GameObject MediaControlPanel;
        public GameObject SectionBeginPrompt;
        public GameObject DirectionsPromt;
        public GameObject SectionCompletePromt;
        #endregion

        private void Start()
        {
            MediaControlPanel.SetActive(false);
            SectionBeginPrompt.SetActive(false);
            DirectionsPromt.SetActive(false);
            SectionCompletePromt.SetActive(false);
        }

        public void DisplayMediaControlPanel(bool display = true)
        {
            if (MediaControlPanel) MediaControlPanel.SetActive(display);
        }

        public void DisplaySectionBeginPrompt(bool display = true)
        {
            if (SectionBeginPrompt) SectionBeginPrompt.SetActive(display);
        }

        public void DisplayDirectionsPrompt(bool display = true)
        {
            if (DirectionsPromt) DirectionsPromt.SetActive(display);
        }

        public void DisplaySectionCompletePrompt(bool display = true)
        {
            if (SectionCompletePromt) SectionCompletePromt.SetActive(display);
        }
    }
}
