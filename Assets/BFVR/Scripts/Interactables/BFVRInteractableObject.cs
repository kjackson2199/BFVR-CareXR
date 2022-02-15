using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    /// <summary>
    /// BFVR Interactable Object. Attach to object with interactable triggers.
    /// </summary>
    /// 
    public class BFVRInteractableObject : MonoBehaviour
    {
        public delegate void OnInteractableTriggered(int triggerId);
        public static OnInteractableTriggered onInteractableTriggered;

        List<GameObject> triggerObjects;

        private void Start()
        {
            BFVRInteractableTrigger[] interactableObjectTriggers = GetComponentsInChildren<BFVRInteractableTrigger>();
            foreach(BFVRInteractableTrigger t in interactableObjectTriggers)
            {
                triggerObjects.Add(t.gameObject);
            }
        }

        private void OnEnable()
        {
            BFVRInteractableTrigger.onTriggerEvent += BFVRInteractableTrigger_onTriggerEvent;
        }

        private void OnDisable()
        {
            BFVRInteractableTrigger.onTriggerEvent -= BFVRInteractableTrigger_onTriggerEvent;
        }

        private void BFVRInteractableTrigger_onTriggerEvent(GameObject triggerObject, int triggerId)
        {
            InteractableTriggerHandle(triggerObject, triggerId);
        }

        protected virtual void InteractableTriggerHandle(GameObject triggerObject, int triggerId)
        {
            if (!triggerObjects.Contains(triggerObject)) return;

            onInteractableTriggered.Invoke(triggerId);
        }
    }
}