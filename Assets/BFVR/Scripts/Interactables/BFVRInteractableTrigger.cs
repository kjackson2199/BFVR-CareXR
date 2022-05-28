using BFVR.InputModule;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BFVR.ChapterManagement;

namespace BFVR.Interactable
{
    [SerializeField]
    public enum InteractableTriggerId
    {
        _1 = 1,
        _2 = 2,
        _3 = 3,
        _4 = 4,
        _5 = 5,
        _6 = 6,
        _7 = 7,
        _8 = 8
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
        public InteractableTriggerId TriggerId = InteractableTriggerId._1;

        public bool StartDisabled = false;

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

        //GameObject grabbedItem;
        BFVRGrabbableObject target;

        Collider _collider;

        private void Start()
        {
            InitializeTrigger();
        }

        private void OnEnable()
        {
            InitializeTrigger();
        }

        private void OnDisable()
        {
            //BFVRGrabbableObject.onGrabbed -= BFVRGrabbable_onGrabbed;
            //BFVRGrabbableObject.onReleased -= BFVRGrabbable_onReleased;
        }

        private void Update()
        {
            if (triggerTripped && !AutoReset) return;
            else if (triggerTripped && AutoReset)
            {
                StartCoroutine(AutoResetTriggerCoroutine());
            }
            else
            {
                if (triggerMode == TriggerMode.Location && target != null)
                {
                    LocationCheck();
                }
                else if (target == null)
                {
                    try
                    {
                        target = BFVRInteractiveStepManager.Instance.GetCurrentStep().InteractableTarget.GetComponent<BFVRGrabbableObject>();
                    }
                    catch
                    {
                        Debug.LogError("Attempted to fix target null issue,Couldnt assign target");
                    }
                }

                CheckTriggerTripped();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            BFVRCollider collider = other.gameObject.GetComponent<BFVRCollider>();
            if(collider && collider.ColliderTag != "")
            {
                if (AllowedTriggerTags.Contains(collider.ColliderTag))
                {
                    touchTriggerTripped = true;

                    BFVRHand hand = collider.gameObject.GetComponentInParent<BFVRHand>();
                    if(hand)
                    {
                        hand.SendHapticsImpulseLow();
                    }
                }
            }
        }

        public void SetTriggerMode(int mode)
        {
            triggerMode = (TriggerMode)mode;
        }

        private void LocationCheck()
        {
            Vector3 targetObjectPos = target.gameObject.transform.position;
            float diff = Mathf.Abs((targetObjectPos - gameObject.transform.position).magnitude);

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
            target = null;
        }

        IEnumerator AutoResetTriggerCoroutine()
        {
            yield return new WaitForSeconds(TriggerAutoResetDelay);
            ResetTrigger();
        }

        void FireTriggerEvent()
        {
            if (triggerTripped) return;

            if(onTriggerEvent != null) onTriggerEvent.Invoke(gameObject, (byte)TriggerId);
            if(onTriggerUEvent != null) onTriggerUEvent.Invoke();
            triggerTripped = true;
        }

        void InitializeTrigger()
        {
            //BFVRGrabbableObject.onGrabbed += BFVRGrabbable_onGrabbed;
            //BFVRGrabbableObject.onReleased += BFVRGrabbable_onReleased;
            if(_collider == null)
            {
                _collider = GetComponent<Collider>();
            }

            if(StartDisabled)
            {
                SetEnableTrigger(false);
            }
        }

        public void SetEnableTrigger(bool enable)
        {
            _collider.enabled = enable;
        }

        public void SetTriggerMode(TriggerMode newMode)
        {
            triggerMode = newMode;
        }

        public void SetLocationTriggerTarget(BFVRGrabbableObject @object)
        {
            target = @object;
        }
    }
}
