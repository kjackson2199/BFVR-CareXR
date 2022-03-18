using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaPanelScreenCoverHost : MonoBehaviour
{
    public static MediaPanelScreenCoverHost medPanelHost;
    public Animator panelAnim;
    public bool panelVisible;
    public bool isPointed = false;

    void Awake()
    {
        panelAnim.SetTrigger("down");
    }

    void Start()
    {
        medPanelHost = this;
        panelVisible = false;
        panelAnim.SetTrigger("down");//sometimes doesn't disappear on awake
    }

    void LateUpdate()
    {
        if (isPointed && !panelVisible)
        {
            panelAnim.SetTrigger("up");
            panelVisible = true;
        }
        else if (!isPointed && panelVisible)
        {
            panelAnim.SetTrigger("down");
            panelVisible = false;
        }
        isPointed = false;
    }
}
