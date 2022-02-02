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
            OnWhenHitTarget     // Laser only activates when hitting a valid target
        }

        public BFVRCursor cursor;
        public float maxLength = 10.0f;

        [SerializeField] private LaserPointerBehaviour _laserBeamBehaviour = LaserPointerBehaviour.OnWhenHitTarget;
        private LineRenderer lineRenderer;

        public LaserPointerBehaviour laserPointerBehaviour
        {
            set
            {
                _laserBeamBehaviour = value;
                if(laserPointerBehaviour == LaserPointerBehaviour.Off || laserPointerBehaviour == LaserPointerBehaviour.OnWhenHitTarget)
                {
                    lineRenderer.enabled = false;
                }
                else
                {
                    lineRenderer.enabled = true;
                }
            }
            get { return _laserBeamBehaviour; }
        }

        LaserPointerBehaviour defaultLaserPointerBehaviour;

        [SerializeField] private LayerMask LaserBlockingLayers;

        [Tooltip("Cursor scale when closest to beam source.")]
        public float cursorCloseScale = .05f;
        [Tooltip("Cursor scale when farthest from beam source.")]
        public float cursorFarScale = .05f;

        bool _hitTarget;

        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        private void Start()
        {
            if (cursor) cursor.gameObject.SetActive(false);

            defaultLaserPointerBehaviour = laserPointerBehaviour;
        }

        private void OnEnable()
        {
            BFVRInputManager.uiOnPointStartEvent += BFVRInputManager_uiOnPointStartEvent;
            BFVRInputManager.uiOnPointCanceledEvent += BFVRInputManager_uiOnPointCanceledEvent;
        }

        private void BFVRInputManager_uiOnPointCanceledEvent()
        {
            laserPointerBehaviour = LaserPointerBehaviour.Off;
        }

        private void BFVRInputManager_uiOnPointStartEvent()
        {
            laserPointerBehaviour = defaultLaserPointerBehaviour;
        }

        private void LateUpdate()
        {
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
            float cursorDist = Mathf.Abs((hitInfo.point - gameObject.transform.position).magnitude);
            float cursorScale = cursorDist / maxLength;

            cursor.SetCursorScale(cursorScale);
            cursor.SetCursorPosition(hitInfo.point);
            cursor.SetCursorFacingNormal(hitInfo.normal);
        }

        void UpdateLaserPointer(RaycastHit hitInfo)
        {
            Vector3 start = gameObject.transform.position;
            Vector3 end = hitInfo.point;

            if(Mathf.Abs(end.magnitude) <= 0)
            {
                end = start + (gameObject.transform.forward * maxLength);
            }

            if(laserPointerBehaviour == LaserPointerBehaviour.Off)
            {
                return;
            }

            else if(laserPointerBehaviour == LaserPointerBehaviour.On)
            {
                if(!lineRenderer.enabled)
                {
                    lineRenderer.enabled = true;
                }

                lineRenderer.SetPosition(0, start);
                lineRenderer.SetPosition(1, end);
            }

            else if(laserPointerBehaviour == LaserPointerBehaviour.OnWhenHitTarget)
            {
                if(_hitTarget)
                {
                    if(!lineRenderer.enabled)
                    {
                        lineRenderer.enabled = true;
                        lineRenderer.SetPosition(0, start);
                        lineRenderer.SetPosition(1, end);
                    }
                }
                else
                {
                    if(lineRenderer.enabled)
                    {
                        lineRenderer.enabled = false;
                    }
                }
            }
        }

        RaycastHit CastBeam()
        {
            RaycastHit hit;
            bool bHit = Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out hit, maxLength, LaserBlockingLayers);
            return hit;
        }
    }
}
