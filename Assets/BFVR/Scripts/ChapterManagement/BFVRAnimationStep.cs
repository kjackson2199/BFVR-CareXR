using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRAnimationStep : MonoBehaviour
    {
        public delegate void OnStepBeginDelegate();
        public delegate void OnStepCompleteDelegate();

        public static event OnStepBeginDelegate onStepBeginEvent;
        public static event OnStepCompleteDelegate onStepCompleteEvent;

        public enum StepActionType { Transform, ObjectAnimation , ResetTransform };
        public enum StepTriggerType { Manual, Autostart };

        [System.Serializable]
        public struct StepAction
        {
            public GameObject target;
            public StepActionType Action;
            public Transform StartTransform;
            public Transform EndTransform;
            public string AnimationStateName;
        }

        public string stepName = "Step 1";
        public List<StepAction> StepActions = new List<StepAction>();
        public StepTriggerType StepTrigger = StepTriggerType.Autostart;
        public float Duration = 1.0f;

        public UnityEvent OnStepBegin;
        public UnityEvent OnStepComplete;

        bool _isPlaying;
        bool _isComplete;
        float _deltaStep;

        private void Start()
        {
            //if (gameObject.activeSelf) gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            if(StepTrigger == StepTriggerType.Autostart)
            {
                PlayStep();
            }
        }

        private void OnDisable()
        {
            Reset();
        }

        private void Update()
        {
            UpdateStepAction();
        }

        void StartStepActions()
        {
            if(OnStepBegin != null) OnStepBegin.Invoke();
            if(onStepBeginEvent != null) onStepBeginEvent.Invoke();

            foreach(StepAction action in StepActions)
            {
                switch(action.Action)
                {
                    case StepActionType.ResetTransform:
                        action.target.transform.rotation = action.EndTransform.rotation;
                        action.target.transform.position = action.EndTransform.position;
                        break;
                    case StepActionType.ObjectAnimation:
                        BFVROrderedObjectAnimation anim = action.target.GetComponent<BFVROrderedObjectAnimation>();
                        if (anim != null && action.AnimationStateName != null) anim.PlayStep(action.AnimationStateName);
                        break;
                        
                }
            }
        }

        void AdvanceStepActions(float delta)
        {
            delta = Mathf.Clamp(delta, 0, 1);
            foreach(StepAction action in StepActions)
            {
                switch(action.Action)
                {
                    case StepActionType.Transform:
                        action.target.transform.position = Vector3.Slerp(action.StartTransform.position, action.EndTransform.position, delta);
                        if (!action.StartTransform.rotation.Equals(action.EndTransform.rotation))
                            action.target.transform.rotation = Quaternion.Slerp(action.StartTransform.rotation, action.EndTransform.rotation, delta);
                        break;
                }
            }
        }

        protected void PauseStepActions() { }

        void UpdateStepAction()
        {
            if (_isPlaying && !_isComplete)
            {
                _deltaStep += Time.deltaTime / Duration;
                AdvanceStepActions(_deltaStep);
                if (_deltaStep >= 1.0f)
                {
                    if(OnStepComplete != null) OnStepComplete.Invoke();
                    if(onStepCompleteEvent != null) onStepCompleteEvent.Invoke();
                    Debug.Log("Animation Step Complete");
                    _isComplete = true;
                }
            }
        }

        void PlayStep()
        {
            _isPlaying = true;
            _isComplete = false;
            _deltaStep = 0.0f;
            StartStepActions();
        }

        void PauseStep()
        {
            _isPlaying = false;
            PauseStepActions();
        }

        void ResumeStep()
        {
            _isPlaying = true;
        }

        void StopStep()
        {
            _isPlaying = false;
            PauseStepActions();
        }

        private void Reset()
        {
            _isPlaying = false;
            _isComplete = false;
            _deltaStep = 0.0f;
        }

        public bool IsStepComplete()
        { return _isComplete; }
        public bool IsStepPlaying()
        { return _isPlaying; }
    }
}
