using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRInteractiveStepManager : MonoBehaviour
    {
        public delegate void OnStartedDelegate();
        public delegate void OnNextStepDelegate();
        public delegate void OnPreviousStepDelegate();
        public delegate void OnCompletedDelgeate();

        public static event OnStartedDelegate onStartedEvent;
        public static event OnNextStepDelegate onNextStepEvent;
        public static event OnPreviousStepDelegate onPreviousStepEvent;
        public static event OnCompletedDelgeate onCompletedEvent;

        public static BFVRInteractiveStepManager Instance { get { return _instance; } }
        private static BFVRInteractiveStepManager _instance;

        public bool autoStart = true;
        public List<BFVRInteractiveStep> Steps = new List<BFVRInteractiveStep>();

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

        public void Start()
        {
            foreach (BFVRInteractiveStep s in Steps)
            {
                s.gameObject.SetActive(false);
            }

            if(autoStart)
            {
                PlaySteps();
            }
        }

        private void OnEnable()
        {
            BFVRInteractiveStep.onStepCompleteEvent += BFVRInteractiveStep_onStepCompleteEvent;
        }

        public void PlaySteps()
        {
            if (onStartedEvent != null) onStartedEvent.Invoke();
            if (OnStarted != null) OnStarted.Invoke();

            NextStep();
        }

        public void NextStep()
        {
            foreach (BFVRInteractiveStep s in Steps)
            {
                s.gameObject.SetActive(false);
            }

            _stepIndex++;
            if (_stepIndex >= Steps.Count)
            {
                StepsComplete();
                return;
            }

            Debug.Log("BFVRInteractiveStepManager: Next Step");
            Steps[_stepIndex].gameObject.SetActive(true);

            if (OnNextStep != null) OnNextStep.Invoke();
            if (onNextStepEvent != null) onNextStepEvent.Invoke();
        }

        public void PreviousStep()
        {
            foreach (BFVRInteractiveStep s in Steps)
            {
                s.gameObject.SetActive(false);
            }

            _stepIndex--;
            if (_stepIndex < 0)
            {
                return;
            }

            Steps[_stepIndex].gameObject.SetActive(true);

            if (OnPreviousStep != null) OnPreviousStep.Invoke();
            if (onPreviousStepEvent != null) onPreviousStepEvent.Invoke();
        }

        void StepsComplete()
        {
            Debug.Log("BFVRInteractiveStepManager: Steps Completed");
            _stepIndex = -1;

            if (onCompletedEvent != null)
                onCompletedEvent.Invoke();

            if (OnCompleted != null)
                OnCompleted.Invoke();
        }

        #region Interactive Step Callback
        private void BFVRInteractiveStep_onStepCompleteEvent()
        {
            NextStep();
        }
        #endregion
    }
}
