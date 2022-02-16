using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.InputModule
{
    public class BFVRHand : MonoBehaviour
    {
        public Transform palmTransform;

        public UnityEvent GrabTest;
        public UnityEvent ReleaseTest;

        private void OnEnable()
        {
            BFVRInputManager.interactionOnGrabRightStartedEvent += BFVRInputManager_interactionOnGrabRightStartedEvent;
            BFVRInputManager.interactionOnGrabRightCanceledEvent += BFVRInputManager_interactionOnGrabRightCanceledEvent;
            BFVRInputManager.interactionOnGrabLeftStartedEvent += BFVRInputManager_interactionOnGrabRightStartedEvent;
            BFVRInputManager.interactionOnGrabLeftCanceledEvent += BFVRInputManager_interactionOnGrabRightCanceledEvent;
        }

        private void OnDisable()
        {
            BFVRInputManager.interactionOnGrabRightStartedEvent -= BFVRInputManager_interactionOnGrabRightStartedEvent;
            BFVRInputManager.interactionOnGrabRightCanceledEvent -= BFVRInputManager_interactionOnGrabRightCanceledEvent;
            BFVRInputManager.interactionOnGrabLeftStartedEvent -= BFVRInputManager_interactionOnGrabRightStartedEvent;
            BFVRInputManager.interactionOnGrabLeftCanceledEvent -= BFVRInputManager_interactionOnGrabRightCanceledEvent;
        }

        private void BFVRInputManager_interactionOnGrabRightCanceledEvent()
        {
            ReleaseTest.Invoke();
        }

        private void BFVRInputManager_interactionOnGrabRightStartedEvent()
        {
            GrabTest.Invoke();
        }

    }
}
