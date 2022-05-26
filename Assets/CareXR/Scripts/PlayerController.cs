using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.LegacyInputHelpers;
using UnityEditor.XR.LegacyInputHelpers;

public class PlayerController : MonoBehaviour
{
    public Collider playerCollider;

    public CameraOffset cameraOffset;

    public void EnableCollider()
    {
        playerCollider.enabled = true;
    }

    public void DisableCollider()
    {
        playerCollider.enabled = false;
    }

    public void MovePlayer(Vector3 newPos, Quaternion newRot)
    {
        transform.position = new Vector3(newPos.x, 0, newPos.z);
        cameraOffset.cameraYOffset = newPos.y;
        transform.rotation = newRot;
    }

}
