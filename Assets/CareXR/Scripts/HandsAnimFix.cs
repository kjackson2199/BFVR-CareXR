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
        BFVRInputManager.
    }
    void OnDisable()
    {
        BFVRInputManager.
    }

    void Update()
    {
        //need check for if pose changed this frame
        if (!gripRight && !triggerRight) rightHand.SetTrigger("Idle");
        else if(gripRight && !triggerRight) rightHand.SetTrigger("Grip");
        else if(!gripRight && triggerRight) rightHand.SetTrigger("Trigger");
        else if(gripRight && triggerRight) rightHand.SetTrigger("Both");

        if (!gripLeft && !triggerLeft) leftHand.SetTrigger("Idle");
        else if(gripLeft && !triggerLeft) leftHand.SetTrigger("Grip");
        else if(!gripLeft && triggerLeft) leftHand.SetTrigger("Trigger");
        else if(gripLeft && triggerLeft) leftHand.SetTrigger("Both");
    }
}
