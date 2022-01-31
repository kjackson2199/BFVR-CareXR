using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TempHand : MonoBehaviour
{
    public XRNode NodeType;
    List<XRNodeState> mNodeStates = new List<XRNodeState>();

    
    IEnumerator Start()
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
        for (int i = 0; i < subsystems.Count; i++)
        {
            while (!subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Device)) yield return null;
            while (!subsystems[i].TryRecenter()) yield return null;
        }
    }
    
    void Update()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;
        InputTracking.GetNodeStates(mNodeStates);

        for (int i = 0; i < mNodeStates.Count; i++)
        {
            if (mNodeStates[i].nodeType == NodeType)
            {
                mNodeStates[i].TryGetPosition(out pos);
                mNodeStates[i].TryGetRotation(out rot);
                break;
            }
        }

        transform.localPosition = pos;
        transform.localRotation = rot;
    }
}
