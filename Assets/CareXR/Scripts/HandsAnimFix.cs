using BFVR.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimFix : MonoBehaviour
{
    public Animator rightHand;
    public Animator leftHand;//hands are set up seperately because array setTrigger also doesn't trigger animations properly.
    //public int state = 0;
    bool gripRight = false;
    bool triggerRight = false;
    bool gripLeft = false;
    bool triggerLeft = false;

    void OnEnable()
    {
        BFVRInputManager.handposesOnGripLeftStartedEvent += BFVRInputManager_handposesOnGripLeftStartedEvent;
        BFVRInputManager.handposesOnGripLeftCanceledEvent += BFVRInputManager_handposesOnGripLeftCanceledEvent;
        BFVRInputManager.handposesOnGripRightStartedEvent += BFVRInputManager_handposesOnGripRightStartedEvent;
        BFVRInputManager.handposesOnGripRightCanceledEvent += BFVRInputManager_handposesOnGripRightCanceledEvent;
        BFVRInputManager.handposesOnPointLeftStartedEvent += BFVRInputManager_handposesOnPointLeftStartedEvent;
        BFVRInputManager.handposesOnPointLeftCanceledEvent += BFVRInputManager_handposesOnPointLeftCanceledEvent;
        BFVRInputManager.handposesOnPointRightStartedEvent += BFVRInputManager_handposesOnPointRightStartedEvent;
        BFVRInputManager.handposesOnPointRightCanceledEvent += BFVRInputManager_handposesOnPointRightCanceledEvent;
    }


    void OnDisable()
    {
        BFVRInputManager.handposesOnGripLeftStartedEvent -= BFVRInputManager_handposesOnGripLeftStartedEvent;
        BFVRInputManager.handposesOnGripLeftCanceledEvent -= BFVRInputManager_handposesOnGripLeftCanceledEvent;
        BFVRInputManager.handposesOnGripRightStartedEvent -= BFVRInputManager_handposesOnGripRightStartedEvent;
        BFVRInputManager.handposesOnGripRightCanceledEvent -= BFVRInputManager_handposesOnGripRightCanceledEvent;
        BFVRInputManager.handposesOnPointLeftStartedEvent -= BFVRInputManager_handposesOnPointLeftStartedEvent;
        BFVRInputManager.handposesOnPointLeftCanceledEvent -= BFVRInputManager_handposesOnPointLeftCanceledEvent;
        BFVRInputManager.handposesOnPointRightStartedEvent -= BFVRInputManager_handposesOnPointRightStartedEvent;
        BFVRInputManager.handposesOnPointRightCanceledEvent -= BFVRInputManager_handposesOnPointRightCanceledEvent;
    }

    void Update()
    {
        //need check for if pose changed this frame
        if (!gripRight && !triggerRight) rightHand.SetTrigger("Idle");
        else if (gripRight && !triggerRight) rightHand.SetTrigger("Grip");
        else if (!gripRight && triggerRight) rightHand.SetTrigger("Trigger");
        else if (gripRight && triggerRight) rightHand.SetTrigger("Both");

        if (!gripLeft && !triggerLeft) leftHand.SetTrigger("Idle");
        else if (gripLeft && !triggerLeft) leftHand.SetTrigger("Grip");
        else if (!gripLeft && triggerLeft) leftHand.SetTrigger("Trigger");
        else if (gripLeft && triggerLeft) leftHand.SetTrigger("Both");
    }

    private void BFVRInputManager_handposesOnPointRightCanceledEvent()
    {
        triggerRight = false;
    }

    private void BFVRInputManager_handposesOnPointRightStartedEvent()
    {
        triggerRight = true;
    }

    private void BFVRInputManager_handposesOnPointLeftCanceledEvent()
    {
        triggerLeft = false;
    }

    private void BFVRInputManager_handposesOnPointLeftStartedEvent()
    {
        triggerLeft = true;
    }

    private void BFVRInputManager_handposesOnGripRightCanceledEvent()
    {
        gripRight = false;
    }

    private void BFVRInputManager_handposesOnGripRightStartedEvent()
    {
        gripRight = true;
    }

    private void BFVRInputManager_handposesOnGripLeftCanceledEvent()
    {
        gripLeft = false;
    }

    private void BFVRInputManager_handposesOnGripLeftStartedEvent()
    {
        gripLeft = true;
    }

}
