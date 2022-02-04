using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR Head Mounted Display tracking controller. Attach to GameObject with XR camera.
    /// </summary>
    public class BFVRHMD : MonoBehaviour
    {
        List<XRNodeState> nodeStates = new List<XRNodeState>();

        IEnumerator Start()
        {
            List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
            for (int i = 0; i < subsystems.Count; i++)
            {
                while (!subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Floor)) yield return null;
                while (!subsystems[i].TryRecenter()) yield return null;
            }
        }

        void Update()
        {
            Vector3 pos = new Vector3();
            Quaternion rot = new Quaternion();

            InputTracking.GetNodeStates(nodeStates);
            foreach (XRNodeState nodeState in nodeStates)
            {
                switch (nodeState.nodeType)
                {
                    case XRNode.Head:
                        nodeState.TryGetPosition(out pos);
                        nodeState.TryGetRotation(out rot);
                        break;
                }
            }

            gameObject.transform.localPosition = pos;
            gameObject.transform.localRotation = rot;
        }
    }
}
