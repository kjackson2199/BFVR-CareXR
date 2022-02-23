using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Standard BFVR 3D Cursor input. Lets you use a world space VR cursor on world space canvases. Attach to EventSystem object.
/// </summary>
namespace BFVR.InputModule
{
    public class BFVR3DCursorInputModule : BaseInputModule
    {
        static BFVR3DCursorInputModule _instance;
        static GameObject targetObject;

        static bool inputClicked;

        protected override void Awake()
        {
            _instance = this;
        }

        protected override void OnEnable()
        {
            BFVRInputManager.uiOnClickStartedEvent += BFVRInputManager_uiOnClickStartedEvent;
        }

        protected override void OnDisable()
        {
            BFVRInputManager.uiOnClickStartedEvent -= BFVRInputManager_uiOnClickStartedEvent;
        }

        private void BFVRInputManager_uiOnClickStartedEvent()
        {
            Process();
        }

        public override void Process()
        {
            if (targetObject == null)
                return;

            inputClicked = false;
            Button b = targetObject.GetComponent<Button>();
            if (b) b.onClick.Invoke();
        }

        public override bool IsPointerOverGameObject(int pointerId)
        {
            if (targetObject) return true;
            return false;
        }

        public static void SetTargetObject(GameObject obj)
        {
            if(targetObject && targetObject != obj)
            {
                PointerEventData pointerEvent = new PointerEventData(_instance.eventSystem);
                pointerEvent.position = BFVRInputManager.Cursor.gameObject.transform.position;
                ExecuteEvents.Execute(targetObject, pointerEvent, ExecuteEvents.pointerExitHandler);
            }

            if(obj)
            {
                if (obj == targetObject)
                    return;

                PointerEventData pointerEvent = new PointerEventData(_instance.eventSystem);
                pointerEvent.pointerEnter = obj;
                pointerEvent.position = BFVRInputManager.Cursor.gameObject.transform.position;

                ExecuteEvents.Execute(obj, pointerEvent, ExecuteEvents.pointerEnterHandler);
            }

            targetObject = obj;
        }
    }
}
