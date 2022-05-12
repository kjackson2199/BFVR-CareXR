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
        public delegate void OnInteractableTriggered(byte interactiveObjectId, int triggerId);
        public static event OnInteractableTriggered onInteractableTriggeredEvent;

        public byte InteractableId = 0;

        public UnityEvent OnInteractableTriggeredUEvent;

        List<GameObject> triggerObjects = new List<GameObject>();

        private void Start()
        {
            BFVRInteractableTrigger[] interactableObjectTriggers = GetComponentsInChildren<BFVRInteractableTrigger>();
            foreach(BFVRInteractableTrigger t in interactableObjectTriggers)
            {
                triggerObjects.Add(t.gameObject);
            }

            //DisableAllTriggers();
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

        public void SetTriggerMode(int TriggerId, int mode)
        {
            foreach (GameObject g in triggerObjects)
            {
                InteractableTriggerId id = g.GetComponent<BFVRInteractableTrigger>().TriggerId;
                if ((int)id == TriggerId)
                {
                    g.GetComponent<BFVRInteractableTrigger>().SetTriggerMode(mode);
                    break;
                }
            }
        }

        public void EnableTrigger(int TriggerId)
        {
            foreach(GameObject g in triggerObjects)
            {
                InteractableTriggerId id = g.GetComponent<BFVRInteractableTrigger>().TriggerId;
                if((int)id == TriggerId)
                {
                    BFVRInteractableTrigger t = g.GetComponent<BFVRInteractableTrigger>();
                    t.SetEnableTrigger(true);
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
                    BFVRInteractableTrigger t = g.GetComponent<BFVRInteractableTrigger>();
                    t.SetEnableTrigger(false);
                    break;
                }
            }
        }

        public void EnableAllTriggers()
        {
            foreach (GameObject g in triggerObjects)
            {
                BFVRInteractableTrigger t = g.GetComponent<BFVRInteractableTrigger>();
                t.SetEnableTrigger(true);
            }
        }

        public void DisableAllTriggers()
        {
            foreach (GameObject g in triggerObjects)
            {
                BFVRInteractableTrigger t = g.GetComponent<BFVRInteractableTrigger>();
                t.SetEnableTrigger(false);
            }
        }

        private void BFVRInteractableTrigger_onTriggerEvent(GameObject triggerObject, int triggerId, GameObject grabbedObject)
        {
            InteractableTriggerHandle(triggerObject, triggerId, grabbedObject);
        }

        protected virtual void InteractableTriggerHandle(GameObject triggerObject, int triggerId, GameObject grabbedObject)
        {
            if (!triggerObjects.Contains(triggerObject)) return;

            if(onInteractableTriggeredEvent != null) onInteractableTriggeredEvent.Invoke(InteractableId, triggerId);
            if(OnInteractableTriggeredUEvent != null) OnInteractableTriggeredUEvent.Invoke();

            Debug.Log("Trigger Id: " + triggerId + "Trigger Object: " + triggerObject.name);
        }
    }
}