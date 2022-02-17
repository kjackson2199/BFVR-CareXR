using BFVR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.InputModule
{
    public class BFVRHand : MonoBehaviour
    {
        public BFVRHandRaycaster handRaycaster;
        [HideInInspector] public Transform palmTransform;
        GameObject itemInHand;

        bool _rightHand;

        private void Awake()
        {
            HandCheck();
        }

        private void Start()
        {
            palmTransform = handRaycaster.transform;
        }

        private void OnEnable()
        {
            if (_rightHand)
            {
                BFVRInputManager.interactionOnGrabRightStartedEvent += BFVRInputManager_interactionOnGrabRightStartedEvent;
                BFVRInputManager.interactionOnGrabRightCanceledEvent += BFVRInputManager_interactionOnGrabRightCanceledEvent;
            }
            else
            {
                BFVRInputManager.interactionOnGrabLeftStartedEvent += BFVRInputManager_interactionOnGrabLeftStartedEvent;
                BFVRInputManager.interactionOnGrabLeftCanceledEvent += BFVRInputManager_interactionOnGrabLeftCanceledEvent;
            }
        }

        private void OnDisable()
        {
            if (_rightHand)
            {
                BFVRInputManager.interactionOnGrabRightStartedEvent -= BFVRInputManager_interactionOnGrabRightStartedEvent;
                BFVRInputManager.interactionOnGrabRightCanceledEvent -= BFVRInputManager_interactionOnGrabRightCanceledEvent;
            }
            else
            {
                BFVRInputManager.interactionOnGrabLeftStartedEvent -= BFVRInputManager_interactionOnGrabLeftStartedEvent;
                BFVRInputManager.interactionOnGrabLeftCanceledEvent -= BFVRInputManager_interactionOnGrabLeftCanceledEvent;
            }
        }

        void HandCheck()
        {
            if (gameObject.CompareTag("Right Hand"))
            {
                _rightHand = true;
            }
        }

        void GrabCheck()
        {
            RaycastHit hit;
            Vector3 avgHitVector = handRaycaster.RaycastClosestHit(out hit);

            if (hit.collider)
            {
                BFVRGrabbableObject g = hit.collider.gameObject.GetComponent<BFVRGrabbableObject>();

                if (g.Grab(this))
                {
                    itemInHand = hit.collider.gameObject;
                }
            }
        }

        public void RemoveItemFromHand()
        {
            Debug.Log("Removed");
            itemInHand = null;
        }

        void ReleaseItemInHand()
        {
            if (!itemInHand) return;

            BFVRGrabbableObject g = itemInHand.GetComponent<BFVRGrabbableObject>();
            g.Release(this);
            itemInHand = null;
        }

        #region Input Interaction Callbacks
        private void BFVRInputManager_interactionOnGrabRightStartedEvent()
        {
            GrabCheck();
        }

        private void BFVRInputManager_interactionOnGrabRightCanceledEvent()
        {
            ReleaseItemInHand();
        }

        private void BFVRInputManager_interactionOnGrabLeftStartedEvent()
        {
            GrabCheck();
        }

        private void BFVRInputManager_interactionOnGrabLeftCanceledEvent()
        {
            ReleaseItemInHand();
        }
        #endregion
    }
}
