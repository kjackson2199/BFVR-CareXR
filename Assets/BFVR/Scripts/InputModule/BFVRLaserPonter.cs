using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR laser pointer. Attach script to empty game object since it uses empty as laser beam source.
    /// </summary>
    /// 
    [RequireComponent(typeof(LineRenderer))]
    public class BFVRLaserPonter : MonoBehaviour
    {
        public enum LaserPointerBehaviour
        {
            On,                 // Laser always on
            Off,                // Laser always off
            OnPlayerPoint     // Laser only activates when player points
        }

        public BFVRCursor cursor;
        public float maxLength = 10.0f;

        private LineRenderer lineRenderer;

        public LaserPointerBehaviour laserPointerBehaviour;

        [SerializeField] private LayerMask LaserBlockingLayers;

        [Tooltip("Cursor scale when closest to beam source.")]
        public float cursorCloseScale = .05f;
        [Tooltip("Cursor scale when farthest from beam source.")]
        public float cursorFarScale = .05f;

        bool _hitTarget;
        bool _pointing;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            if (!cursor) cursor.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            BFVRInputManager.uiOnPointStartEvent += BFVRInputManager_uiOnPointStartEvent;
            BFVRInputManager.uiOnPointCanceledEvent += BFVRInputManager_uiOnPointCanceledEvent;

        }

        private void OnDisable()
        {
            BFVRInputManager.uiOnPointStartEvent -= BFVRInputManager_uiOnPointStartEvent;
            BFVRInputManager.uiOnPointCanceledEvent -= BFVRInputManager_uiOnPointCanceledEvent;
        }

        private void BFVRInputManager_uiOnPointCanceledEvent()
        {
            _pointing = false;
        }

        private void BFVRInputManager_uiOnPointStartEvent()
        {
            _pointing = true;
        }

        private void LateUpdate()
        {
            switch(laserPointerBehaviour)
            {
                case LaserPointerBehaviour.Off:
                    LaserPointerOff();
                    return;
                case LaserPointerBehaviour.On:
                    LaserPointerOn();
                    break;
                case LaserPointerBehaviour.OnPlayerPoint:
                    if (!_pointing)
                    {
                        LaserPointerOff();
                        return;
                    }
                    else
                    {
                        LaserPointerOn();
                        break;
                    }
            }

            RaycastHit hit = CastBeam();
            UpdateLaserPointer(hit);
            UpdateCursor(hit);
        }

        void UpdateCursor(RaycastHit hitInfo)
        {
            if(!cursor)
            {
                return;
            }

            Vector3 cursorPos = Mathf.Abs(hitInfo.point.magnitude) <= 0 ? gameObject.transform.position + (gameObject.transform.forward * maxLength) : hitInfo.point;
            Vector3 cursorNormal = Mathf.Abs(hitInfo.point.magnitude) <= 0 ? gameObject.transform.forward : hitInfo.normal;
            cursorPos += .025f * cursorNormal;
            float cursorDist = Mathf.Abs((hitInfo.point - gameObject.transform.position).magnitude);
            float cursorScale = cursorDist / maxLength;

            cursor.SetCursorScale(cursorScale);
            cursor.SetCursorPosition(cursorPos);
            cursor.SetCursorFacingNormal(cursorNormal);
        }

        void UpdateLaserPointer(RaycastHit hitInfo)
        {
            Vector3 start = gameObject.transform.position;
            Vector3 end = Mathf.Abs(hitInfo.point.magnitude) <= 0 ? start + (gameObject.transform.forward * maxLength) : hitInfo.point;

            lineRenderer.SetPosition(0, start);
            lineRenderer.SetPosition(1, end);
        }

        RaycastHit CastBeam()
        {
            RaycastHit hit;
            bool bHit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, maxLength, LaserBlockingLayers);
            return hit;
        }

        void LaserPointerOn()
        {
            if (lineRenderer.enabled && cursor.gameObject.activeSelf)
            {
                return;
            }

            lineRenderer.enabled = true;
            cursor.gameObject.SetActive(true);
        }

        void LaserPointerOff()
        {
            if(!lineRenderer.enabled && !cursor.gameObject.activeSelf)
            {
                return;
            }

            lineRenderer.enabled = false;
            cursor.gameObject.SetActive(false);
        }
    }
}
