using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsAnimFix : MonoBehaviour
{
    public Animator rightHand;
    public Animator leftHand;//hands are set up seperately because array setTrigger also doesn't trigger animations properly.
    int state = 0;

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
}
