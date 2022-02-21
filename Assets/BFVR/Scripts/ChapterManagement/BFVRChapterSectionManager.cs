using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BFVRChapterSectionManager : MonoBehaviour
{
    public delegate void OnSectionBeginDelegate();
    //public delegate void OnSectionCompleteDelegate();
    public delegate void OnSectionResetDelegate();

    public static event OnSectionBeginDelegate onSectionBeginEvent;
    //public static event OnSectionCompleteDelegate onSectionCompleteEvent;
    public static event OnSectionResetDelegate onSectionResetEvent;

    public UnityEvent OnSectionBegin;
    //public UnityEvent OnSectionCompleteEvent;
    public UnityEvent OnSectionReset;

    public virtual void BeginSection()
    {
        if (onSectionBeginEvent != null) onSectionBeginEvent.Invoke();
        if (OnSectionBegin != null) OnSectionBegin.Invoke();
    }

    public virtual void ResetSection()
    {
        if (onSectionResetEvent != null) onSectionResetEvent.Invoke();
        if (OnSectionReset != null) OnSectionReset.Invoke();
    }
}
