using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BFVR.Interactable;

public class PictureInPicture : MonoBehaviour
{
    public Animator anim;
    public BFVRHighlight highlight;
    float stepStartTime;
    float flashStartTime;
    bool showing = false;
    bool hi = false;

    void Update()
    {
        if (!showing && Time.time > stepStartTime+15)//make the item appear after x seconds.
        {
            showing = true;
            anim.SetTrigger("Show");
        }
        if (showing && !hi && Time.time > flashStartTime+0.5f)//make the highlight flash while the item is active.
        {
            highlight.ShowHightlight();//this function is misspelled in the highlight script. I don't want to fix it because all the steps in all scenes would have to be updated.
            hi = true;
        }
        if (showing && hi && Time.time > flashStartTime+1)
        {
            highlight.HideHighlight();
            hi = false;
            flashStartTime = Time.time;
        }
    }

    public void SetStepTime()
    {
        showing = false;
        stepStartTime = Time.time;
        highlight.HideHighlight();
        anim.SetTrigger("Hide");
    }
}
