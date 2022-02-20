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
        public InteractableTriggerId TargetTrigger;

        [Space]
        public AudioClip stepCompleteSFX;

        [Header("Step Events")]
        public UnityEvent OnStepBeginEvent;
        public UnityEvent OnStepCompleteEvent;

        private void OnEnable()
        {
            BFVRInteractableObject.onInteractableTriggeredEvent += BFVRInteractableObject_onInteractableTriggered;
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
        }

        public void CompleteStep()
        {
            if(OnStepCompleteEvent != null) OnStepCompleteEvent.Invoke();
            if(onStepCompleteEvent != null) onStepCompleteEvent.Invoke();

            BFVRAudioManager.PlaySFX(stepCompleteSFX);
        }

        private void BFVRInteractableObject_onInteractableTriggered(GameObject owningObject, int triggerId)
        {
            if(owningObject != InteractableTarget && triggerId != (int)TargetTrigger)
            {
                return;
            }

            CompleteStep();
        }
    }
}
