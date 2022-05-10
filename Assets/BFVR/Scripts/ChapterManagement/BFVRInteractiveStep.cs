using BFVR.AudioModule;
using BFVR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.ChapterManagement
{
    public class BFVRInteractiveStep : MonoBehaviour
    {
        public delegate void OnStepBeginDelegate();
        public delegate void OnStepCompleteDelegate();
        public delegate void OnStepResetDelegate();

        public static event OnStepBeginDelegate onStepBeginEvent;
        public static event OnStepCompleteDelegate onStepCompleteEvent;
        public static event OnStepResetDelegate onStepResetEvent;

        [Header("Step Settings")]
        public string InteractiveStepName = "Step 1";
        public BFVRInteractableObject InteractableTarget;
        public byte InteractableObjectId = 0;
        public InteractableTriggerId TargetTrigger;
        public float StepBeginDelay = 1;

        [Space]
        public AudioClip stepCompleteSFX;

        [Header("Step Events")]
        public UnityEvent OnStepBeginEvent;
        public UnityEvent OnStepBeginDelayedEvent;
        public UnityEvent OnStepCompleteEvent;
        public UnityEvent OnStepResetEvent;

        private void OnEnable()
        {
            BeginInteractiveStep();
        }

        private void OnDisable()
        {
            BFVRInteractableObject.onInteractableTriggeredEvent -= BFVRInteractableObject_onInteractableTriggered;
        }

        public void BeginInteractiveStep() 
        {
            if (OnStepBeginEvent != null) OnStepBeginEvent.Invoke();
            if (onStepBeginEvent != null) onStepBeginEvent.Invoke();
            if (OnStepBeginDelayedEvent != null) StartCoroutine(BeginDelay());
        }

        public void CompleteStep()
        {
            if (!gameObject.activeSelf) return;

            if(OnStepCompleteEvent != null) OnStepCompleteEvent.Invoke();
            if(onStepCompleteEvent != null) onStepCompleteEvent.Invoke();

            BFVRAudioManager.PlaySFX(stepCompleteSFX);
        }

        public void Reset()
        {
            StopAllCoroutines();
            if(OnStepResetEvent != null) OnStepResetEvent.Invoke();
        }

        private void BFVRInteractableObject_onInteractableTriggered(byte interactiveObjectId, int triggerId)
        {
            //if(interactiveObjectId != InteractableObjectId || triggerId != (int)TargetTrigger)
            //{
            //    return;
            //}

            if(interactiveObjectId == InteractableObjectId && triggerId == (int)TargetTrigger)
            {
                CompleteStep();
            }
        }

        IEnumerator BeginDelay()
        {
            yield return new WaitForSeconds(StepBeginDelay);
            BFVRInteractableObject.onInteractableTriggeredEvent += BFVRInteractableObject_onInteractableTriggered;
            OnStepBeginDelayedEvent.Invoke();
        }
    }
}
