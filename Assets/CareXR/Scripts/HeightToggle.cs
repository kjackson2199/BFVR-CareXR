using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BFVR;

public class HeightToggle : MonoBehaviour
{
    public Image buttonImage;
    public Sprite standingSprite;
    public Sprite sittingSprite;
    bool sitting = false;

    void Start()
    {
        if (BFVRApp.playerSitting)
        {
            buttonImage.sprite = sittingSprite;
            sitting = true;
        }
    }
    
    public void ToggleSit()
    {
        if (!sitting)
        {
            sitting = true;
            buttonImage.sprite = sittingSprite;
            GameObject.Find("BFVRApp").transform.position += new Vector3(0,0.5f,0);//trying to find gameobject on awake sometimes finds the incorrect headset
            BFVRApp.playerSitting = true;
        }
        else
        {
            sitting = false;
            buttonImage.sprite = standingSprite;
            GameObject.Find("BFVRApp").transform.position -= new Vector3(0,0.5f,0);
            BFVRApp.playerSitting = false;
        }
    }
}
