using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimFixTest : MonoBehaviour
{
    public Animator anim;
    bool animating = false;

    void Update()
    {
        if (Input.GetKeyDown("g"))
        {
            if (animating)
            {
                animating = false;
                anim.SetTrigger("Stop");
            }
            else
            {
                animating = true;
                anim.SetTrigger("Start");
            }
        }
    }
}
