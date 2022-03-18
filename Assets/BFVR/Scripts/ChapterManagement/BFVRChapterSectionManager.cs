using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BFVRChapterSectionManager : MonoBehaviour
{
    public delegate void OnSectionBeginDelegate();
    //public delegate void OnSectionCompleteDelegate();
    public delegate void OnSectionResetDelegate();
    public delegate void OnSectionEndDelegate();

    public static event OnSectionBeginDelegate onSectionBeginEvent;
    //public static event OnSectionCompleteDelegate onSectionCompleteEvent;
    public static event OnSectionResetDelegate onSectionResetEvent;
    public static event OnSectionEndDelegate onSectionEndEvent;

    public UnityEvent OnSectionBegin;
    //public UnityEvent OnSectionCompleteEvent;
    public UnityEvent OnSectionReset;
    public UnityEvent OnSectionEnd;

    public virtual void BeginSection()
    {
        if (onSectionBeginEvent != null) onSectionBeginEvent.Invoke();
        if (OnSectionBegin != null) OnSectionBegin.Invoke();
    }

    public virtual void EndSection()
    {
        if (onSectionEndEvent != null) onSectionEndEvent.Invoke();
        if (OnSectionEnd != null) OnSectionEnd.Invoke();
    }

    public virtual void ResetSection()
    {
        if (onSectionResetEvent != null) onSectionResetEvent.Invoke();
        if (OnSectionReset != null) OnSectionReset.Invoke();
    }
}
