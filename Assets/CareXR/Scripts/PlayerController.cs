using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.LegacyInputHelpers;
using UnityEditor.XR.LegacyInputHelpers;
using BFVR.InputModule;
using UnityEngine.UI;
using OVR;

public class PlayerController : MonoBehaviour
{
    public Collider playerCollider;

    //public OVRManager cameraRig;

    public PlayerStart playerStartLocation;

    bool aPressed = false;

    public float recenterTimer = 5;

    public GameObject recenterCountdown;
    public Image recenterFillCircle;

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
        //transform.position = new Vector3(newPos.x, 0, newPos.z);
        //cameraOffset.cameraYOffset = newPos.y;
        transform.position = newPos;
        transform.rotation = newRot;
    }

    public void SetPlayerStartLocation(PlayerStart playerStart)
    {
        playerStartLocation = playerStart;
    }

    void RecenterCamera()
    {
        MovePlayer(playerStartLocation.transform.position, playerStartLocation.transform.rotation);
    }

    private void OnEnable()
    {
        BFVRInputManager.RecenterOnPressStartedEvent += BFVRInputManager_recenterOnPressStartedEvent;
        BFVRInputManager.RecenterOnPressCanceledEvent += BFVRInputManager_recenterOnPressCanceledEvent;
        OVRManager.display.RecenteredPose += RecenterCamera;
    }

    private void BFVRInputManager_recenterOnPressStartedEvent()
    {
        recenterCountdown.SetActive(true);
        recenterFillCircle.fillAmount = 0;
        //StartCoroutine("StartRecenter");
    }

    private void BFVRInputManager_recenterOnPressCanceledEvent()
    {
        recenterCountdown.SetActive(false);
        recenterFillCircle.fillAmount = 0;
        //StopCoroutine("StartRecenter");
    }

    IEnumerator StartRecenter()
    {
        Debug.Log("RecenterCoroutineStarted");
        float time = 0;

        while(time <= recenterTimer)
        {
            time += Time.deltaTime;
            float fillAmount = time / recenterTimer;
            recenterFillCircle.fillAmount = fillAmount;
            yield return null;
        }

        RecenterCamera();
    }

}
