using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRChapterManager : MonoBehaviour
    {
        public delegate void OnChapterBeginDelegate();
        public delegate void OnNextSectionDelegate();
        public delegate void OnPreviousSectionDelegate();
        public delegate void OnEndChapterDelegate();

        public static event OnChapterBeginDelegate onChapterBeginEvent;
        public static event OnNextSectionDelegate onNextSectionEvent;
        public static event OnPreviousSectionDelegate onPreviousSectionEvent;
        public static event OnEndChapterDelegate onEndChapterEvent;

        public List<BFVRChapterSectionManager> ChapterSections;

        public BFVRAnimationStepManager AnimationStepManager;
        public BFVRInteractiveStepManager InteractiveStepManager;

        public UnityEvent OnChapterBegin;
        public UnityEvent OnNextSection;
        public UnityEvent OnPreviousSection;
        public UnityEvent OnChapterComplete;

        int _sectionIndex = -1;

        private void Start()
        {
            BeginChapter();
        }

        public void BeginChapter()
        {
            if (onChapterBeginEvent != null) onChapterBeginEvent.Invoke();
            if (OnChapterBegin != null) OnChapterBegin.Invoke();

            NextSection();
        }

        public void NextSection()
        {
            if(_sectionIndex + 1 >= ChapterSections.Count)
            {
                EndChapter();
            }

            _sectionIndex++;
            ChapterSections[_sectionIndex].BeginSection();

            if (OnNextSection != null) OnNextSection.Invoke();
            if (onNextSectionEvent != null) onNextSectionEvent.Invoke();
        }

        public void PreviousSection()
        {
            if (_sectionIndex - 1 < 0)
            {
                return;
            }

            _sectionIndex--;
            ChapterSections[_sectionIndex].BeginSection();

            if (OnPreviousSection != null) OnPreviousSection.Invoke();
            if (onPreviousSectionEvent != null) onPreviousSectionEvent.Invoke();
        }

        public void ResetCurrentSection()
        {
            if (_sectionIndex < 0 || _sectionIndex >= ChapterSections.Count) return;
            ChapterSections[_sectionIndex].ResetSection();
        }

        public void EndChapter()
        {
            _sectionIndex = -1;

            if (OnChapterComplete != null) OnChapterComplete.Invoke();
            if (onEndChapterEvent != null) onEndChapterEvent.Invoke();
        }

        public void PlayAnimationSteps()
        {
            if(AnimationStepManager) AnimationStepManager.NextStep();
        }

        public void PlayInteractiveSteps()
        {
            if (InteractiveStepManager) InteractiveStepManager.PlaySteps();
        }
    }
}
