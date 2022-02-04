using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.InputModule
{
    public class BFVRPlayerMovementController : MonoBehaviour
    {
        protected enum TurnSnapSelection
        {
            _15 = 15,
            _25 = 25,
            _45 = 45,
            _90 = 90
        }

        public float MovementSpeed = .1f;
        [Tooltip("Player with snap to rotation interval and not rotate at a constant rate when turn input is recieved.")]
        public bool UseTurnSnapping = true;
        [SerializeField] protected TurnSnapSelection TurnSnapAmount = TurnSnapSelection._15;
        [Range(0, 90)] public float TurnRate = 45;

        bool turnSnapped;

        Rigidbody body;

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            ApplyPlayerInput();
        }

        private void OnEnable()
        {
            BFVRInputManager.movementOnTurnSnapPerformedEvent += BFVRInputManager_movementOnTurnSnapPerformedEvent;
        }

        private void BFVRInputManager_movementOnTurnSnapPerformedEvent(float value)
        {
            float v = Mathf.Sign(value);
            Vector3 targetRotation = (int)TurnSnapAmount * Vector3.up * v;
            gameObject.transform.Rotate(targetRotation, Space.Self);
        }

        void ApplyPlayerInput()
        {
            Vector3 forwardVel = BFVRInputManager.moveForwardValue * gameObject.transform.forward * Time.deltaTime * MovementSpeed;
            Vector3 rightVel = BFVRInputManager.moveRightValue * gameObject.transform.right * Time.deltaTime * MovementSpeed;

            Vector3 movVel = forwardVel + rightVel;
            body.velocity = movVel;

            if(!UseTurnSnapping)
            {
                Vector3 targetRotation = BFVRInputManager.turnValue * TurnRate * Vector3.up * Time.deltaTime;
                gameObject.transform.Rotate(targetRotation, Space.Self);
            }
        }
    }
}