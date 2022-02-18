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

        public enum StepManagerState { InActive, Active, Complete };
        public StepManagerState State = StepManagerState.InActive;

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

        void NextStep()
        {
            if(State == StepManagerState.Complete)
            {
                Debug.Log("BFVRAnimationStepManager: Steps completed.");
                return;
            }

            else if(State == StepManagerState.InActive)
            {
                State = StepManagerState.Active;
            }

            if(_stepIndex >= Steps.Count)
            {
                StepsComplete();
            }
            else
            {
                _stepIndex++;
                Steps[_stepIndex].gameObject.SetActive(true);
            }
        }

        void PreviousStep()
        {
            if(State == StepManagerState.Complete)
            {
                Debug.Log("BFVRAnimationStepManager: Steps completed.");
                return;
            }
            
            else if(State == StepManagerState.InActive)
            {
                State = StepManagerState.Active;
            }

            if(_stepIndex > 0)
            {
                Steps[_stepIndex].gameObject.SetActive(false);
                _stepIndex--;
                Steps[_stepIndex].gameObject.SetActive(true);
            }
        }

        void StepsComplete()
        {
            State = StepManagerState.Complete;
            onCompletedEvent.Invoke();
            OnCompleted.Invoke();
        }

        #region Animation Step Callback
        private void BFVRAnimationStep_onStepCompleteEvent()
        {
            Steps[_stepIndex].gameObject.SetActive(false);
        }
        #endregion
    }
}