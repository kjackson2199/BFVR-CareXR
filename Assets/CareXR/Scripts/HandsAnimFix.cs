using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimFix : MonoBehaviour
{
    public Animator rightHand;
    public Animator leftHand;//hands are set up seperately because array setTrigger also doesn't trigger animations properly.
    public int state = 0;

    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            if (state == 0)
            {
                rightHand.SetTrigger("Grip");
                leftHand.SetTrigger("Grip");
                state++;
            }
            else if (state == 1)
            {
                rightHand.SetTrigger("Trigger");
                leftHand.SetTrigger("Trigger");
                state++;
            }
            else if (state == 2)
            {
                rightHand.SetTrigger("Both");
                leftHand.SetTrigger("Both");
                state++;
            }
            else if (state == 3)
            {
                rightHand.SetTrigger("Idle");
                leftHand.SetTrigger("Idle");
                state = 0;
            }
        }
    }

    public void AnimateHand(bool rightHand, string triggerName)//bool: true for right hand, false for left hand. String: enter the trigger you want to call on the animator.
    {
        if (rightHand) rightHand.SetTrigger(triggerName);
        else leftHand.SetTrigger(triggerName);
    }
}
