using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using System;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR Head Mounted Display tracking controller. Attach to GameObject with XR camera.
    /// </summary>
    public class BFVRHMD : MonoBehaviour
    {
        public TrackingOriginModeFlags trackingMode = TrackingOriginModeFlags.Device;

        List<XRNodeState> nodeStates = new List<XRNodeState>();

        IEnumerator Start()
        {
            List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
            SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
            for (int i = 0; i < subsystems.Count; i++)
            {
                Debug.Log(subsystems[i].GetSupportedTrackingOriginModes());

                TrackingOriginModeFlags modes = subsystems[i].GetSupportedTrackingOriginModes();

                foreach (TrackingOriginModeFlags x in Enum.GetValues(typeof(TrackingOriginModeFlags)))
                {
                    if (modes.HasFlag(x))
                    {
                        Debug.Log($"Tracking Origin Mode {x} supported, enum value {(int)x}");
                    }
                }

                while (!subsystems[i].TrySetTrackingOriginMode(trackingMode)) yield return null;
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

            pos.y = 0;
            gameObject.transform.localPosition = pos;
            gameObject.transform.localRotation = rot;
        }
    }
}
