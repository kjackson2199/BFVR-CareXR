// GENERATED AUTOMATICALLY FROM 'Assets/BFVR/Scripts/InputModule/InputActionMaps/StandardAppInterface.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace BFVR.InputModule
{
    public class @StandardAppInterface : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @StandardAppInterface()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""StandardAppInterface"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""e90230c2-0396-431f-bd38-f88e93c80b61"",
            ""actions"": [
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Value"",
                    ""id"": ""46016f8f-ed91-4e1a-a2e6-f3bfb2cc262e"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Value"",
                    ""id"": ""ced3b6b4-1948-4328-9883-3c1c93eb0eb0"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""Value"",
                    ""id"": ""35d8a405-a1da-4aed-bdab-78c7c60d4346"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TurnSnap"",
                    ""type"": ""Value"",
                    ""id"": ""10f36088-cd5e-4fcd-8568-48475a8ec001"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6c4c562e-009e-488b-9c81-1d82428ed673"",
                    ""path"": ""<OculusTouchController>{LeftHand}/thumbstick/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4192daea-a6e4-4542-b187-b01ead1733ba"",
                    ""path"": ""<OculusTouchController>{LeftHand}/thumbstick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f392ef2-abec-46d3-b80a-17d7a7bd3b9c"",
                    ""path"": ""<OculusTouchController>{RightHand}/thumbstick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""614195d9-18b6-4f44-a048-1af79367d032"",
                    ""path"": ""<OculusTouchController>{RightHand}/thumbstick/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TurnSnap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""845a0ace-ce7b-4322-8306-4afda4b926a5"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""30632b0e-116b-426a-ab05-a328059705d5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""Button"",
                    ""id"": ""0d7d31ab-1d03-430e-b6e6-d20907654d43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""11945608-806c-4e39-bed9-0231b2003eb3"",
                    ""path"": ""<XRController>{RightHand}/triggerPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fb69310-ff30-495c-846a-50e58e200b7a"",
                    ""path"": ""<XRController>{RightHand}/gripPressed"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Movement
            m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
            m_Movement_MoveForward = m_Movement.FindAction("MoveForward", throwIfNotFound: true);
            m_Movement_MoveRight = m_Movement.FindAction("MoveRight", throwIfNotFound: true);
            m_Movement_Turn = m_Movement.FindAction("Turn", throwIfNotFound: true);
            m_Movement_TurnSnap = m_Movement.FindAction("TurnSnap", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
            m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // Movement
        private readonly InputActionMap m_Movement;
        private IMovementActions m_MovementActionsCallbackInterface;
        private readonly InputAction m_Movement_MoveForward;
        private readonly InputAction m_Movement_MoveRight;
        private readonly InputAction m_Movement_Turn;
        private readonly InputAction m_Movement_TurnSnap;
        public struct MovementActions
        {
            private @StandardAppInterface m_Wrapper;
            public MovementActions(@StandardAppInterface wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveForward => m_Wrapper.m_Movement_MoveForward;
            public InputAction @MoveRight => m_Wrapper.m_Movement_MoveRight;
            public InputAction @Turn => m_Wrapper.m_Movement_Turn;
            public InputAction @TurnSnap => m_Wrapper.m_Movement_TurnSnap;
            public InputActionMap Get() { return m_Wrapper.m_Movement; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
            public void SetCallbacks(IMovementActions instance)
            {
                if (m_Wrapper.m_MovementActionsCallbackInterface != null)
                {
                    @MoveForward.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveForward;
                    @MoveForward.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveForward;
                    @MoveForward.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveForward;
                    @MoveRight.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                    @MoveRight.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                    @MoveRight.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMoveRight;
                    @Turn.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurn;
                    @Turn.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurn;
                    @Turn.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurn;
                    @TurnSnap.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurnSnap;
                    @TurnSnap.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurnSnap;
                    @TurnSnap.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnTurnSnap;
                }
                m_Wrapper.m_MovementActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveForward.started += instance.OnMoveForward;
                    @MoveForward.performed += instance.OnMoveForward;
                    @MoveForward.canceled += instance.OnMoveForward;
                    @MoveRight.started += instance.OnMoveRight;
                    @MoveRight.performed += instance.OnMoveRight;
                    @MoveRight.canceled += instance.OnMoveRight;
                    @Turn.started += instance.OnTurn;
                    @Turn.performed += instance.OnTurn;
                    @Turn.canceled += instance.OnTurn;
                    @TurnSnap.started += instance.OnTurnSnap;
                    @TurnSnap.performed += instance.OnTurnSnap;
                    @TurnSnap.canceled += instance.OnTurnSnap;
                }
            }
        }
        public MovementActions @Movement => new MovementActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Click;
        private readonly InputAction m_UI_Point;
        public struct UIActions
        {
            private @StandardAppInterface m_Wrapper;
            public UIActions(@StandardAppInterface wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_UI_Click;
            public InputAction @Point => m_Wrapper.m_UI_Point;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                    @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                    @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @Point.started += instance.OnPoint;
                    @Point.performed += instance.OnPoint;
                    @Point.canceled += instance.OnPoint;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IMovementActions
        {
            void OnMoveForward(InputAction.CallbackContext context);
            void OnMoveRight(InputAction.CallbackContext context);
            void OnTurn(InputAction.CallbackContext context);
            void OnTurnSnap(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnPoint(InputAction.CallbackContext context);
        }
    }
}
