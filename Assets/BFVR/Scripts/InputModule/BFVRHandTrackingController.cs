using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR hand tracking controller. Keeps track of user's hand position using the OpenXR framework.
    /// </summary>
    public class BFVRHandTrackingController : MonoBehaviour
    {
        Transform HandRoot;
        Vector3 HandPos;
        Quaternion HandRotation;

        bool _rightHand;

        private void Start()
        {
            HandCheck();
            InitializeXRSubsystem();
        }

        private void Update()
        {
            UpdateHandTracking();
        }

        void HandCheck()
        {
            if (gameObject.CompareTag("Right Hand"))
            {
                _rightHand = true;
            }
        }

        void InitializeXRSubsystem()
        {
            List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
            for(int i = 0; i <subsystems.Count;i++)
            {
                subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor);
            }
        }

        void UpdateHandTracking()
        {
            HandPos = Vector3.zero;
            HandRotation = Quaternion.identity;

            List<XRNodeState> nodeStates = new List<XRNodeState>();
            InputTracking.GetNodeStates(nodeStates);

            foreach(XRNodeState nodeState in nodeStates)
            {
                if(_rightHand && nodeState.nodeType == XRNode.RightHand)
                {
                    nodeState.TryGetPosition(out HandPos);
                    nodeState.TryGetRotation(out HandRotation);
                    break;
                }
                else if(!_rightHand && nodeState.nodeType == XRNode.LeftHand)
                {
                    nodeState.TryGetPosition(out HandPos);
                    nodeState.TryGetRotation(out HandRotation);
                    break;
                }
            }

            transform.localPosition = HandPos;
            transform.localRotation = HandRotation;
        }
    }
}
