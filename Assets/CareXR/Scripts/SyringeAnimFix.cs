using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeAnimFix : MonoBehaviour
{
    public Animator anim;

    void OnEnable()
    {
        //BFVRInputManager.
    }
    void OnDisable()
    {
        //BFVRInputManager.
    }

    void PlayAnim(string stateName)
    {
        anim.SetTrigger(stateName);//change trigger to match animator
    }
}
