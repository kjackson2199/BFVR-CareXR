using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRAnimationStepManager : MonoBehaviour
    {
        public delegate void OnStartedDelegate();
        public delegate void OnNextStepDelegate();
        public delegate void OnPreviousStepDelegate();
        public delegate void OnCompletedDelgeate();

        public static event OnStartedDelegate onStartedEvent;
        public static event OnNextStepDelegate onNextStepEvent;
        public static event OnPreviousStepDelegate onPreviousStepEvent;
        public static event OnCompletedDelgeate onCompletedEvent;

        public static BFVRAnimationStepManager Instance { get { return _instance; } }
        private static BFVRAnimationStepManager _instance;

        public bool autoStart = true;
        public List<BFVRAnimationStep> Steps = new List<BFVRAnimationStep>();

        public UnityEvent OnStarted;
        public UnityEvent OnNextStep;
        public UnityEvent OnPreviousStep;
        public UnityEvent OnCompleted;

        int _stepIndex = -1;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        private void OnEnable()
        {
            BFVRAnimationStep.onStepCompleteEvent += BFVRAnimationStep_onStepCompleteEvent;
        }

        public void NextStep()
        {
            foreach(BFVRAnimationStep s in Steps)
            {
                s.gameObject.SetActive(false);
            }

            _stepIndex++;
            if(_stepIndex >= Steps.Count)
            {
                StepsComplete();
                return;
            }

            Steps[_stepIndex].gameObject.SetActive(true);
        }

        public void PreviousStep()
        {
            foreach(BFVRAnimationStep s in Steps)
            {
                s.gameObject.SetActive(false);
            }

            _stepIndex--;
            if(_stepIndex < 0) 
            {
                return;
            }

            Steps[_stepIndex].gameObject.SetActive(true);
        }

        void StepsComplete()
        {
            Debug.Log("Steps Completed");
            _stepIndex = -1;

            if (onCompletedEvent != null)
                onCompletedEvent.Invoke();

            if (OnCompleted != null)
                OnCompleted.Invoke();
        }

        #region Animation Step Callback
        private void BFVRAnimationStep_onStepCompleteEvent()
        {
            NextStep();
        }
        #endregion
    }
}