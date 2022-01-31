using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class XRHMD : MonoBehaviour
{
    public Transform head;
    public Transform leftHandObject;
    public Transform rightHandObject;

    public InputAction cheatWASD;
    public InputAction cheatUpDown;

    List<XRNodeState> nodeStates = new List<XRNodeState>();

    Vector3 pos;
    Quaternion rot;

    private void OnEnable()
    {
        //cheatWASD.Enable();
    }
    private void OnDisable()
    {
        //cheatWASD.Disable();
    }
    IEnumerator Start()
    {
        List<XRInputSubsystem> subsystems = new List<XRInputSubsystem>();
        SubsystemManager.GetInstances<XRInputSubsystem>(subsystems);
        for (int i = 0; i < subsystems.Count; i++)
        {
            while (!subsystems[i].TrySetTrackingOriginMode(TrackingOriginModeFlags.Device)) yield return null;
            while(!subsystems[i].TryRecenter()) yield return null;
        }
    }

    void Update()
    {
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

        head.localPosition = pos;
        head.localRotation = rot;

        Vector2 move = cheatWASD.ReadValue<Vector2>();
        float up = cheatUpDown.ReadValue<float>();

        transform.position += new Vector3(move.x * 0.5f, up * 0.5f, move.y * 0.5f)*Time.deltaTime;
    }
}
