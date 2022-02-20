using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BFVR.Interactable
{
    /// <summary>
    /// BFVR Interactable Object. Attach to object with interactable triggers.
    /// </summary>
    /// 
    public class BFVRInteractableObject : MonoBehaviour
    {
        public delegate void OnInteractableTriggered(GameObject owningObject, int triggerId);
        public static event OnInteractableTriggered onInteractableTriggeredEvent;

        public UnityEvent OnInteractableTriggeredUEvent;

        List<GameObject> triggerObjects = new List<GameObject>();

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

        public void ResetTriggers()
        {
            foreach(GameObject g in triggerObjects)
            {
                g.GetComponent<BFVRInteractableTrigger>().ResetTrigger();
            }
        }

        public void EnableTrigger(int TriggerId)
        {
            foreach(GameObject g in triggerObjects)
            {
                InteractableTriggerId id = g.GetComponent<BFVRInteractableTrigger>().TriggerId;
                if((int)id == TriggerId)
                {
                    g.gameObject.SetActive(true);
                    break;
                }
            }
        }

        public void DisableTrigger(int TriggerId)
        {
            foreach (GameObject g in triggerObjects)
            {
                InteractableTriggerId id = g.GetComponent<BFVRInteractableTrigger>().TriggerId;
                if ((int)id == TriggerId)
                {
                    g.gameObject.SetActive(false);
                    break;
                }
            }
        }

        private void BFVRInteractableTrigger_onTriggerEvent(GameObject triggerObject, int triggerId, GameObject grabbedObject)
        {
            InteractableTriggerHandle(triggerObject, triggerId, grabbedObject);
        }

        protected virtual void InteractableTriggerHandle(GameObject triggerObject, int triggerId, GameObject grabbedObject)
        {
            if (!triggerObjects.Contains(triggerObject)) return;

            if(onInteractableTriggeredEvent != null) onInteractableTriggeredEvent.Invoke(gameObject, triggerId);
            if(OnInteractableTriggeredUEvent != null) OnInteractableTriggeredUEvent.Invoke();

            Debug.Log("Trigger Id: " + triggerId);
        }
    }
}