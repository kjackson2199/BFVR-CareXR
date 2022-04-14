using BFVR.Interactable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

namespace BFVR.InputModule
{
    public class BFVRHand : MonoBehaviour
    {
        public BFVRHandRaycaster handRaycaster;
        [HideInInspector] public Transform palmTransform;
        public BFVROrderedObjectAnimation animooa;
        public Renderer handRenderer;
        public Outline outline;
        public string OpacityAttributeName;

        Material handMaterial;
        float defaultHandOpacity;
        GameObject itemInHand;
        bool _rightHand;

        InputDevice XRDevice;
        HapticCapabilities deviceCapabilities;

        private void Awake()
        {
            HandCheck();
        }

        private void Start()
        {
            palmTransform = handRaycaster.transform;
            handMaterial = handRenderer.material;

            if (handMaterial != null)
            {
                defaultHandOpacity = handMaterial.GetFloat(OpacityAttributeName);
            }

            XRHapticsSetup();
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

        #region Grabbing
        void GrabCheck()
        {
            RaycastHit hit;
            Vector3 avgHitVector = handRaycaster.RaycastClosestHit(out hit);

            if (hit.collider)
            {
                HideHand();
                BFVRGrabbableObject g = hit.collider.gameObject.GetComponent<BFVRGrabbableObject>();

                if (g.Grab(this))
                {
                    itemInHand = hit.collider.gameObject;
                    SendHapticImpulseMed();
                }
            }
        }

        public void RemoveItemFromHand()
        {
            itemInHand = null;

            SendHapticImpulseMed();
        }

        void ReleaseItemInHand()
        {
            if (!itemInHand) return;

            BFVRGrabbableObject g = itemInHand.GetComponent<BFVRGrabbableObject>();
            g.Release(this);
            itemInHand = null;

            SendHapticImpulseMed();
        }
        #endregion

        void HideHand()
        {
            if(handMaterial != null)
            {
                StartCoroutine(MaterialOpacityFadeCoroutine(0));
                if (outline != null) outline.enabled = false;
            }
        }

        void ShowHand()
        {
            if(handMaterial != null)
            {
                StartCoroutine(MaterialOpacityFadeCoroutine(defaultHandOpacity));
                if (outline != null) outline.enabled = true;
            }
        }

        IEnumerator MaterialOpacityFadeCoroutine(float newOpacity, float fadeTime = 1)
        {
            float delta = 0;
            float opacity = 0;

            if(!handRenderer.enabled && newOpacity > 0)
            {
                handRenderer.enabled = true;
            }

            while (delta < fadeTime)
            {
                opacity = Mathf.Lerp(handMaterial.GetFloat(OpacityAttributeName), newOpacity, delta / fadeTime);
                handMaterial.SetFloat(OpacityAttributeName, opacity);

                yield return new WaitForEndOfFrame();
                delta += Time.deltaTime;
            }

            if (newOpacity <= 0)
            {
                handRenderer.enabled = false;
            }
            

            delta = 1;
            opacity = Mathf.Lerp(handMaterial.GetFloat(OpacityAttributeName), newOpacity, delta / fadeTime);
        }

        #region Haptics
        void XRHapticsSetup()
        {
            if (_rightHand)
            {
                // Find and Set XR Device as right hand
                XRDevice = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            }
            else
            {
                // Find and set XR device as left hand
                XRDevice = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
            }

            XRDevice.TryGetHapticCapabilities(out deviceCapabilities);
        }

        public void SendHapticsImpulseLow()
        {
            SendHapticImpulse(.33f);
        }

        public void SendHapticImpulseMed()
        {
            SendHapticImpulse(.66f);
        }

        public void SendHapticImpulseHigh()
        {
            SendHapticImpulse();
        }

        public void SendHapticImpulse(float amplitude = 1, float duration = .1f)
        {
            if(deviceCapabilities.supportsImpulse)
            {
                uint channel = 0;
                XRDevice.SendHapticImpulse(channel, amplitude, duration);
            }
        }
        #endregion

        #region Input Interaction Callbacks
        private void BFVRInputManager_interactionOnGrabRightStartedEvent()
        {
            GrabCheck();
        }

        private void BFVRInputManager_interactionOnGrabRightCanceledEvent()
        {
            ReleaseItemInHand();
            ShowHand();
        }

        private void BFVRInputManager_interactionOnGrabLeftStartedEvent()
        {
            GrabCheck();
        }

        private void BFVRInputManager_interactionOnGrabLeftCanceledEvent()
        {
            ReleaseItemInHand();
            ShowHand();
        }
        #endregion
    }
}
