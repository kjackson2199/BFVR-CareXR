using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureInPicture : MonoBehaviour
{
    public Animator anim;
    float stepStartTime;
    float flashStartTime;
    bool showing = false;
    bool hi = false;
    string animNameSet = "Hide";

    void Update()
    {
        if (!showing && Time.time > stepStartTime+15)//make the play the set animation after after 15 seconds.
        {
            showing = true;
            anim.SetTrigger(animNameSet);
        }
    }

    public void SetStepTime(string animName)//Calling this at the start of every interactable step will cause picture-in-picture to trigger the entered animation.
    {
        showing = false;
        stepStartTime = Time.time;
        anim.SetTrigger("Hide");
        animNameSet = animName;
    }
}
