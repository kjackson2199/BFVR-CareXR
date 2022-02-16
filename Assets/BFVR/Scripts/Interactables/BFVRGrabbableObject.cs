using BFVR.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.Interactable
{
    /// <summary>
    /// BFVR grabbable object. Attach to object to allow user to grab.
    /// </summary>
    /// 
    public class BFVRGrabbableObject : MonoBehaviour
    {
        public delegate void OnGrabDelegate(GameObject @object);
        public delegate void OnReleaseDelegate(GameObject @object);

        public static event OnGrabDelegate onGrabbed;
        public static event OnReleaseDelegate onReleased;

        public bool EnableGrabbable;
        public bool UseHandedness;  // Not implemented
        [Tooltip("Allow user to grab item from other hand.")]
        public bool AllowGrabSwap;
        [Tooltip("If user releases grabbable return to original position. False if easing is used.")]
        public bool ReturnToOriginalPositionOnRelease = true;
        [Tooltip("If user releases grabbable snap to new position. False if easing is used.")]
        public bool SnapToPositionOnRelease = true;
        [Range(.1f, 1f)] public float FadeTime = .25f;

        #region UnityEvents
        [Header("Events")]
        public UnityEvent OnGrab;
        public UnityEvent OnRelease;
        #endregion

        bool _itemGrabbed;

        BFVRHand _currentHand;

        Vector3 originalPosition;
        Quaternion originalRotation;

        public void Start()
        {
            SetNewOriginalTransform();
        }

        public void SetNewOriginalTransform()
        {
            originalPosition = gameObject.transform.position;
            originalRotation = gameObject.transform.rotation;
        }


        public void Grab(BFVRHand parentHand)
        {
            if (_itemGrabbed && AllowGrabSwap && _currentHand != parentHand)
            {
                goto DO_GRAB;
            }
            else if(_itemGrabbed && !AllowGrabSwap)
            {
                return;
            }

        DO_GRAB:
            _currentHand = parentHand;
            gameObject.transform.position = parentHand.palmTransform.position;
            gameObject.transform.rotation = parentHand.palmTransform.rotation;

            gameObject.transform.parent = _currentHand.transform;

            onGrabbed.Invoke(_currentHand.gameObject);
            OnGrab.Invoke();
        }

        public void Release(Vector3 newPos = new Vector3(), Quaternion newRot = new Quaternion(), bool snapToNewTransform = false)
        {
            gameObject.transform.parent = null;

            if(snapToNewTransform)
            {
                if(SnapToPositionOnRelease)
                {
                    gameObject.transform.position = newPos;
                    gameObject.transform.rotation = newRot;
                }
                else
                {
                    StartCoroutine(ReleaseEasing(newPos, newRot));
                }
            }
            else
            {
                if(SnapToPositionOnRelease)
                {
                    gameObject.transform.position = originalPosition;
                    gameObject.transform.rotation = originalRotation;
                }
                else
                {
                    StartCoroutine(ReleaseEasing(originalPosition, originalRotation));
                }
            }
        }

        IEnumerator ReleaseEasing(Vector3 Pos, Quaternion Rot)
        {
            float timeElapsed = 0;
            Vector3 startingPos = gameObject.transform.position;
            Quaternion startingRot = gameObject.transform.rotation;

            while(timeElapsed < FadeTime)
            {
                Vector3 p = Vector3.Lerp(startingPos, Pos, timeElapsed / FadeTime);
                gameObject.transform.position = p;
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }

            gameObject.transform.position = Pos;
            gameObject.transform.rotation = Rot;
        }
    }
}
