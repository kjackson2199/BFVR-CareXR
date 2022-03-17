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
        [HideInInspector] public bool UseHandedness;  // Not implemented
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

        Coroutine releaseEasingCoroutine;

        Collider collider;

        GameObject emptyParent;

        public void Start()
        {
            SetNewOriginalTransform();
            collider = GetComponent<Collider>();

            GameObject g = new GameObject();
            emptyParent = Instantiate(g, gameObject.transform.position, gameObject.transform.rotation);

            transform.parent = emptyParent.transform;
        }

        public void SetNewOriginalTransform()
        {
            originalPosition = gameObject.transform.position;
            originalRotation = gameObject.transform.rotation;
        }

        public void SetEnableGrabbable(bool enable)
        {
            EnableGrabbable = enable;
            collider.enabled = enable;
        }

        public bool Grab(BFVRHand parentHand)
        {
            if (!EnableGrabbable) return false;

            if (_itemGrabbed && AllowGrabSwap)
            {
                // Item hand swap
                Debug.Log("Swap");
                _currentHand.RemoveItemFromHand();
                goto DO_GRAB;
            }
            else if(_itemGrabbed && !AllowGrabSwap)
            {
                // No swap
                Debug.Log("No Swap");
                return false;
            }
            else if(!_itemGrabbed)
            {
                Debug.Log("Grab");
                goto DO_GRAB;
            }
            else
            {
                return false;
            }

        DO_GRAB:
            if (releaseEasingCoroutine != null)
            {
                StopCoroutine(releaseEasingCoroutine);
            }

            _currentHand = parentHand;
            _itemGrabbed = true;
            gameObject.transform.position = parentHand.palmTransform.position;
            gameObject.transform.rotation = parentHand.palmTransform.rotation;

            gameObject.transform.parent = _currentHand.transform;

            if (onGrabbed != null) onGrabbed.Invoke(gameObject);
            if (OnGrab != null) OnGrab.Invoke();
            return true;
        }

        public void ForceReleaseNewTransform(Transform newTransform)
        {
            if (_currentHand == null) return;

            gameObject.transform.parent = emptyParent.transform;

            if (SnapToPositionOnRelease)
            {
                gameObject.transform.position = newTransform.position;
                gameObject.transform.rotation = newTransform.rotation;
            }
            else
            {
                releaseEasingCoroutine = StartCoroutine(ReleaseEasing(newTransform.position, newTransform.rotation));
            }

            _currentHand = null;
            _itemGrabbed = false;

            if (onReleased != null) onReleased.Invoke(_currentHand.gameObject);
            if (OnRelease != null) OnRelease.Invoke();
        }

        public void Release(BFVRHand hand)
        {
            if (_currentHand != hand || _currentHand == null) return;

            _currentHand.RemoveItemFromHand();
            gameObject.transform.parent = emptyParent.transform;

            if (SnapToPositionOnRelease)
            {
                gameObject.transform.position = originalPosition;
                gameObject.transform.rotation = originalRotation;
            }
            else
            {
                releaseEasingCoroutine = StartCoroutine(ReleaseEasing(originalPosition, originalRotation));
            }

            _currentHand = null;
            _itemGrabbed = false;

            if (onReleased != null)  onReleased.Invoke(gameObject);
            if (OnRelease != null)  OnRelease.Invoke();
        }

        public void ForceRelease()
        {
            if (_currentHand == null) return;

            _currentHand.RemoveItemFromHand();
            gameObject.transform.parent = null;

            if (SnapToPositionOnRelease)
            {
                gameObject.transform.position = originalPosition;
                gameObject.transform.rotation = originalRotation;
            }
            else
            {
                releaseEasingCoroutine = StartCoroutine(ReleaseEasing(originalPosition, originalRotation));
            }

            _currentHand = null;
            _itemGrabbed = false;

            if(onReleased != null) onReleased.Invoke(_currentHand.gameObject);
            if(OnRelease != null) OnRelease.Invoke();
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
