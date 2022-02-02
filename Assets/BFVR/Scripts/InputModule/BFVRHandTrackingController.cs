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
        [Tooltip("Set true for right hand controls. Defaults to left hand.")]
        public bool RightHand = false;

        Transform HandRoot;
        Vector3 HandPos;
        Quaternion HandRotation;

        private void Start()
        {
            InitializeXRSubsystem();
        }

        private void Update()
        {
            UpdateHandTracking();
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
                if(RightHand && nodeState.nodeType == XRNode.RightHand)
                {
                    nodeState.TryGetPosition(out HandPos);
                    nodeState.TryGetRotation(out HandRotation);
                    break;
                }
                else if(!RightHand && nodeState.nodeType == XRNode.LeftHand)
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
