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
        public BFVROrderedObjectAnimation animooa;
        public Renderer handRenderer;
        public Outline outline;
        public string OpacityAttributeName;

        Material handMaterial;
        float defaultHandOpacity;
        GameObject itemInHand;
        bool _rightHand;

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
                HideHand();
                BFVRGrabbableObject g = hit.collider.gameObject.GetComponent<BFVRGrabbableObject>();

                if (g.Grab(this))
                {
                    itemInHand = hit.collider.gameObject;
                }
            }
        }

        public void RemoveItemFromHand()
        {
            itemInHand = null;
        }

        void ReleaseItemInHand()
        {
            if (!itemInHand) return;

            HideHand();
            BFVRGrabbableObject g = itemInHand.GetComponent<BFVRGrabbableObject>();
            g.Release(this);
            itemInHand = null;
        }

        IEnumerator MaterialOpacityFadeCoroutine(float newOpacity, float fadeTime = 1)
        {
            float delta = 0;
            float opacity = 0;

            while(delta < fadeTime)
            {
                opacity = Mathf.Lerp(handMaterial.GetFloat(OpacityAttributeName), newOpacity, delta / fadeTime);
                handMaterial.SetFloat(OpacityAttributeName, opacity);

                yield return new WaitForEndOfFrame();
                delta += Time.deltaTime;
            }

            delta = 1;
            opacity = Mathf.Lerp(handMaterial.GetFloat(OpacityAttributeName), newOpacity, delta / fadeTime);
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
