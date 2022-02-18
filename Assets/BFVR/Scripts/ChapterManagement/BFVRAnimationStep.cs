using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRAnimationStep : MonoBehaviour
    {
        public enum StepActionType { Transform, ObjectAnimation , ResetTransform };
        public enum StepTriggerType { Manual, UserInitiated, Autostart, Timed };

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
            PlayStep();
            StartCoroutine(PauseTest());
        }

        private void Update()
        {
            UpdateStepAction();
        }

        void StartStepActions()
        {
            if (OnStepBegin != null) OnStepBegin.Invoke();

            foreach(StepAction action in StepActions)
            {
                switch(action.Action)
                {
                    case StepActionType.ResetTransform:
                        action.target.transform.rotation = action.EndTransform.rotation;
                        action.target.transform.position = action.EndTransform.position;
                        break;
                    case StepActionType.ObjectAnimation:
                        //Handle Animation
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
                    if (!_isComplete && OnStepComplete != null) OnStepComplete.Invoke();
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

        IEnumerator PauseTest()
        {
            yield return new WaitForSeconds(5);
            PauseStep();
            yield return new WaitForSeconds(5);
            ResumeStep();
        }
    }
}
