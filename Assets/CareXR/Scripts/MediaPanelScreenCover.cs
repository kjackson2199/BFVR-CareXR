using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediaPanelScreenCover : MonoBehaviour
{
    public void PointAtScreen()
    {
        MediaPanelScreenCoverHost.medPanelHost.isPointed = true;
    }
}
