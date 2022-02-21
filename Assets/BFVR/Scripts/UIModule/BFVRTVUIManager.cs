using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.UIModule
{
    public class BFVRTVUIManager : MonoBehaviour
    {
        #region TV Interface Prompt
        public GameObject TVScreenMesh;

        [Space]
        public GameObject ChapterSelect;

        [Space]
        public GameObject MediaControlPanel;
        public GameObject WatchSectionBeginPrompt;
        public GameObject DirectionsPromt;
        public GameObject WatchSectionCompletePrompt;
        public GameObject InteractiveSectionBeginPrompt;
        public GameObject InteractiveSectionCompletePrompt;
        #endregion

        private void Start()
        {
            HidePrompts();
        }

        public void HidePrompts()
        {
            MediaControlPanel.SetActive(false);
            WatchSectionBeginPrompt.SetActive(false);
            DirectionsPromt.SetActive(false);
            WatchSectionCompletePrompt.SetActive(false);
            InteractiveSectionBeginPrompt.SetActive(false);
            InteractiveSectionCompletePrompt.SetActive(false);
        }

        public void DisplayChapterSelectMenu()
        {
            HidePrompts();
            if (ChapterSelect) ChapterSelect.SetActive(true);
        }

        public void DisplayMediaControlPanel()
        {
            HidePrompts();
            if (MediaControlPanel) MediaControlPanel.SetActive(true);
        }

        public void DisplaySectionBeginPrompt()
        {
            HidePrompts();
            if (WatchSectionBeginPrompt) WatchSectionBeginPrompt.SetActive(true);
        }

        public void DisplayDirectionsPrompt()
        {
            HidePrompts();
            if (DirectionsPromt) DirectionsPromt.SetActive(true);
        }

        public void DisplaySectionCompletePrompt()
        {
            HidePrompts();
            if (WatchSectionCompletePrompt) WatchSectionCompletePrompt.SetActive(true);
        }

        public void DisplayInteractiveSectionBeginPrompt()
        {
            HidePrompts();
            if (InteractiveSectionBeginPrompt) InteractiveSectionBeginPrompt.SetActive(true);
        }

        public void DisplayInteractiveSectionCompletePrompt()
        {
            HidePrompts();
            if (InteractiveSectionCompletePrompt) InteractiveSectionCompletePrompt.SetActive(true);
        }
    }
}
