using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static BFVR.InputModule.StandardAppInterface;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR input manager. Manages input actions from selected action maps. (Does not destroy on load.)
    /// </summary>
    /// 
    public class BFVRInputManager : MonoBehaviour , IUIActions , IMovementActions
    {
        #region UI/Actions
        // UI/Point
        public delegate void UI_OnPointStartedDelegate();
        public delegate void UI_OnPointCanceledDelegate();
        public static event UI_OnPointStartedDelegate uiOnPointStartEvent;
        public static event UI_OnPointCanceledDelegate uiOnPointCanceledEvent;

        // UI/Click
        public delegate void UI_OnClickPerformedDelegate();
        public static event UI_OnClickPerformedDelegate uiOnClickPerformedEvent;

        #endregion

        #region Movement/Actions
        public static float moveForwardValue = 0;
        public static float moveRightValue = 0;
        public static float turnValue = 0;

        public delegate void Movement_OnTurnSnapPerformedDelegate(float value);
        public static event Movement_OnTurnSnapPerformedDelegate movementOnTurnSnapPerformedEvent;
        #endregion

        // Default Input Actions Asset
        public StandardAppInterface StandardAppActions;

        public static BFVRCursor Cursor;

        // Singleton
        private static BFVRInputManager _instance;
        public static BFVRInputManager Instance { get { return _instance; } }

        private void Awake()
        {
            if(_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                _instance = this;
            }

            DontDestroyOnLoad(this.gameObject);

            StandardAppActions = new StandardAppInterface();

            StandardAppActions.UI.SetCallbacks(this);
            StandardAppActions.Movement.SetCallbacks(this);

            Cursor = FindObjectOfType<BFVRCursor>();
        }

        private void OnEnable()
        {
            StandardAppActions.UI.Enable();
            StandardAppActions.Movement.Enable();
        }

        private void OnDisable()
        {
            StandardAppActions.UI.Disable();
            StandardAppActions.Movement.Disable();
        }

        #region UI Action Map Interface Implementation

        public void OnPoint(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                uiOnPointStartEvent.Invoke();
            }
            else if(context.canceled)
            {
                uiOnPointCanceledEvent.Invoke();
            }
        }

        public void OnClick(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                uiOnClickPerformedEvent.Invoke();
            }
        }
        #endregion

        #region Movement Action Map Interface Implementation
        public void OnMoveForward(InputAction.CallbackContext context)
        {
            moveForwardValue = context.ReadValue<float>();
        }

        public void OnMoveRight(InputAction.CallbackContext context)
        {
            moveRightValue = context.ReadValue<float>();
        }

        public void OnTurn(InputAction.CallbackContext context)
        {
            turnValue = context.ReadValue<float>();
        }

        public void OnTurnSnap(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                movementOnTurnSnapPerformedEvent.Invoke(context.ReadValue<float>());
            }
        }
        #endregion
    }
}
