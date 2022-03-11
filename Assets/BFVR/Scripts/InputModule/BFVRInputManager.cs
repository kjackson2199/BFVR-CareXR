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
    public class BFVRInputManager : MonoBehaviour , IUIActions , IMovementActions , IInteractionActions , IHandPosesActions
    {
        #region UI/Actions
        // UI/Point
        public delegate void UI_OnPointStartedDelegate();
        public delegate void UI_OnPointCanceledDelegate();
        public static event UI_OnPointStartedDelegate uiOnPointStartEvent;
        public static event UI_OnPointCanceledDelegate uiOnPointCanceledEvent;

        // UI/Click
        public delegate void UI_OnClickStartedDelegate();
        public static event UI_OnClickStartedDelegate uiOnClickStartedEvent;
        public delegate void UI_OnClickCanceledDelegate();
        public static event UI_OnClickCanceledDelegate uiOnClickCanceledEvent;


        #endregion

        #region Movement/Actions
        public static float moveForwardValue = 0;
        public static float moveRightValue = 0;
        public static float turnValue = 0;

        public delegate void Movement_OnTurnSnapPerformedDelegate(float value);
        public static event Movement_OnTurnSnapPerformedDelegate movementOnTurnSnapPerformedEvent;
        #endregion

        #region Interaction Actions

        // Interaction/GrabLeft
        public delegate void Interaction_OnGrabLeftStartedDelegate();
        public delegate void Interaction_OnGrabLeftCanceledDelegate();
        public static event Interaction_OnGrabLeftStartedDelegate interactionOnGrabLeftStartedEvent;
        public static event Interaction_OnGrabLeftCanceledDelegate interactionOnGrabLeftCanceledEvent;

        // Interaction/GrabRight
        public delegate void Interaction_OnGrabRightStartedDelegate();
        public delegate void Interaction_OnGrabRightCanceledDelegate();
        public static event Interaction_OnGrabRightStartedDelegate interactionOnGrabRightStartedEvent;
        public static event Interaction_OnGrabRightCanceledDelegate interactionOnGrabRightCanceledEvent;
        #endregion

        #region Hand Poses Action Map Interface Implementation

        public delegate void HandPoses_OnGripLeftStartedDelegate();
        public delegate void HandPoses_OnGripLeftCanceledDelegate();
        public delegate void HandPoses_OnGripRightStartedDelegate();
        public delegate void HandPoses_OnGripRightCanceledDelegate();
        public delegate void HandPoses_OnPointLeftStartedDelegate();
        public delegate void HandPoses_OnPointLeftCanceledDelegate();
        public delegate void HandPoses_OnPointRightStartedDelegate();
        public delegate void HandPoses_OnPointRightCanceledDelegate();

        // This is that new shit that'll get you going

        public static event HandPoses_OnGripLeftStartedDelegate handposesOnGripLeftStartedEvent;
        public static event HandPoses_OnGripLeftCanceledDelegate handposesOnGripLeftCanceledEvent;
        public static event HandPoses_OnGripRightStartedDelegate handposesOnGripRightStartedEvent;
        public static event HandPoses_OnGripRightCanceledDelegate handposesOnGripRightCanceledEvent;
        public static event HandPoses_OnPointLeftStartedDelegate handposesOnPointLeftStartedEvent;
        public static event HandPoses_OnPointLeftCanceledDelegate handposesOnPointLeftCanceledEvent;
        public static event HandPoses_OnPointRightStartedDelegate handposesOnPointRightStartedEvent;
        public static event HandPoses_OnPointRightCanceledDelegate handposesOnPointRightCanceledEvent;

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
            StandardAppActions.Interaction.SetCallbacks(this);
            StandardAppActions.HandPoses.SetCallbacks(this);

            Cursor = FindObjectOfType<BFVRCursor>();
        }

        private void OnEnable()
        {
            StandardAppActions.UI.Enable();
            StandardAppActions.Movement.Enable();
            StandardAppActions.Interaction.Enable();
            StandardAppActions.HandPoses.Enable();
        }

        private void OnDisable()
        {
            StandardAppActions.UI.Disable();
            StandardAppActions.Movement.Disable();
            StandardAppActions.Interaction.Disable();
            StandardAppActions.HandPoses.Disable();
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
            if(context.started)
            {
                uiOnClickStartedEvent.Invoke();
            }
            else if(context.canceled)
            {
                uiOnPointCanceledEvent.Invoke();
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

        #region Interaction Action Map Interface
        public void OnGrabLeft(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                interactionOnGrabLeftStartedEvent.Invoke();
            }
            else if(context.canceled)
            {
                interactionOnGrabLeftCanceledEvent.Invoke();
            }
        }

        public void OnGrabRight(InputAction.CallbackContext context)
        {
            if(context.started)
            {
                interactionOnGrabRightStartedEvent.Invoke();
            }
            else if(context.canceled)
            {
                interactionOnGrabRightCanceledEvent.Invoke();
            }
        }

        #endregion

        #region Hand Poses Action Map Interface Implementation
        public void OnGripLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                handposesOnGripLeftStartedEvent.Invoke();
            }

            else if (context.canceled)
            {
                handposesOnGripLeftCanceledEvent.Invoke();
            }
        }

        public void OnGripRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                handposesOnGripRightStartedEvent.Invoke();
            }

            else if (context.canceled)
            {
                handposesOnGripRightCanceledEvent.Invoke();
            }
        }

        public void OnPointLeft(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                handposesOnPointLeftStartedEvent.Invoke();
            }

            else if (context.canceled)
            {
                handposesOnPointLeftCanceledEvent.Invoke();
            }
        }

        public void OnPointRight(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                handposesOnPointRightStartedEvent.Invoke();
            }

            else if (context.canceled)
            {
                handposesOnPointRightCanceledEvent.Invoke();
            }
        }

        #endregion
    }
}
