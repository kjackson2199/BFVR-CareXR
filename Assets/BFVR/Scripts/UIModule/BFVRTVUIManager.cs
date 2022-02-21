using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace BFVR.UIModule
{
    public class BFVRTVUIManager : MonoBehaviour
    {
        #region TV Interface Prompt

        [Space]
        public GameObject ChapterSelect;

        [Space]
        public GameObject MediaControlPanel;
        public GameObject WatchSectionBeginPrompt;
        public GameObject DirectionsPromt;
        public GameObject WatchSectionCompletePrompt;
        public GameObject InteractiveSectionBeginPrompt;
        public GameObject InteractiveSectionCompletePrompt;

        [Space]
        [Header("VideoPlayer")]
        public GameObject TVScreenMesh;
        public VideoPlayer vidPlayer;
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


        ////Video Player functionality////
        public void PlayVideo(VideoClip v)
        {
            vidPlayer.clip = v;
            vidPlayer.Play();
        }
        public void PauseVideo()
        {
            vidPlayer.Pause();//unsure if play after pause resumes playback because there is no available unpause method. Should be fine. If video restarts, disable setting clip again above.
        }
        public void StopVideo()
        {
            vidPlayer.Stop();
            vidPlayer.clip = null;//video player sometimes won't load a new video clip before playing again if the last one stopped before it was finished. This prevents that by unloading the previous videoclip.
        }
    }
}
