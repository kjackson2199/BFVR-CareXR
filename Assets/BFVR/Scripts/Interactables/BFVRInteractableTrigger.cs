using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.Interactable
{
    [SerializeField]
    public enum InteractableTriggerIdMask
    {
        _1 = 0x01,
        _2 = 0x02,
        _3 = 0x04,
        _4 = 0x08,
        _5 = 0x10,
        _6 = 0x20,
        _7 = 0x40,
        _8 = 0x80
    }

    public class BFVRInteractableTrigger : MonoBehaviour
    {
        public delegate void OnTriggerDelegate(GameObject triggerObject, int triggerId, GameObject grabbedItem = null);

        public enum TriggerMode
        {
            Touch,      // Trigger when user touches trigger collider
            Location    // Trigger when user places object within range of trigger
        }

        #region Inspector Variables

        [Header("Trigger Settings")]
        public TriggerMode triggerMode;
        public InteractableTriggerIdMask TriggerId = InteractableTriggerIdMask._1;

        public List<string> AllowedTriggerTags = new List<string>();
        [HideInInspector] public string TriggerTag = ""; //Un-used
        [Range(.001f, .1f)] public float TriggerActivationRange = .001f;

        [Tooltip("Automatically reset trigger state once fired.")]
        [Space] public bool AutoReset;
        [Range(.1f,1)] public float TriggerAutoResetDelay = .1f;

        public static event OnTriggerDelegate onTriggerEvent;
        public UnityEvent onTriggerUEvent;

        #endregion

        bool triggerTripped;
        bool touchTriggerTripped;
        bool locationTriggerTripped;

        GameObject grabbedItem;

        private void OnEnable()
        {
            InitializeTrigger();
        }

        private void OnDisable()
        {
            BFVRGrabbableObject.onGrabbed -= BFVRGrabbable_onGrabbed;
            BFVRGrabbableObject.onReleased -= BFVRGrabbable_onReleased;
        }

        private void Update()
        {
            if (triggerTripped && !AutoReset) return;
            else if(triggerTripped && AutoReset)
            {
                StartCoroutine(AutoResetTriggerCoroutine());
            }

            if (triggerMode == TriggerMode.Location && grabbedItem)
            {
                LocationCheck();
            }

            CheckTriggerTripped();
        }

        private void OnTriggerEnter(Collider other)
        {
            BFVRCollider collider = other.gameObject.GetComponent<BFVRCollider>();
            if(collider && collider.ColliderTag != "")
            {
                if (AllowedTriggerTags.Contains(collider.ColliderTag))
                {
                    touchTriggerTripped = true;
                    
                }
            }
        }

        private void LocationCheck()
        {
            Vector3 grabbedItemPos = grabbedItem.transform.position;
            float diff = Mathf.Abs((grabbedItemPos - gameObject.transform.position).magnitude);

            if(diff <= TriggerActivationRange)
            {
                locationTriggerTripped = true;
            }
        }

        void CheckTriggerTripped()
        {
            switch(triggerMode)
            {
                case TriggerMode.Touch:
                    if (touchTriggerTripped) FireTriggerEvent();
                    break;

                case TriggerMode.Location:
                    if (locationTriggerTripped) FireTriggerEvent();
                    break;
            }
        }

        public void ResetTrigger()
        {
            triggerTripped = false;
            touchTriggerTripped = false;
            locationTriggerTripped = false;
            grabbedItem = null;
        }

        IEnumerator AutoResetTriggerCoroutine()
        {
            yield return new WaitForSeconds(TriggerAutoResetDelay);
            ResetTrigger();
        }

        void FireTriggerEvent()
        {
            if(onTriggerEvent != null) onTriggerEvent.Invoke(gameObject, (byte)TriggerId, grabbedItem);
            if(onTriggerUEvent != null) onTriggerUEvent.Invoke();
            triggerTripped = true;
        }

        void InitializeTrigger()
        {
            BFVRGrabbableObject.onGrabbed += BFVRGrabbable_onGrabbed;
            BFVRGrabbableObject.onReleased += BFVRGrabbable_onReleased;
        }
        public void SetTriggerMode(TriggerMode newMode)
        {
            triggerMode = newMode;
        }

        private void BFVRGrabbable_onGrabbed(GameObject @object)
        {
            grabbedItem = @object;
        }

        private void BFVRGrabbable_onReleased(GameObject @object)
        {
            grabbedItem = null;
        }
    }
}
