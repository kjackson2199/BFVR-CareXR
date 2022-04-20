using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BFVR;

public class HeightToggle : MonoBehaviour
{
    public Image buttonImage;
    //public Sprite standingSprite;
    //public Sprite sittingSprite;
    public Sprite oneSprite;
    public Sprite twoSprite;
    public Sprite threeSprite;
    public Sprite fourSprite;
    int state = 1;

    void Start()
    {
        state = BFVRApp.playerHeightState;
        if (state == 2)
        {
            GameObject.Find("BFVRApp").transform.position += new Vector3(0,0.2f,0);//trying to find gameobject on awake sometimes finds the incorrect headset, so we get it in Start instead.
            buttonImage.sprite = twoSprite;
            }
        else if (state == 3)
        {
            GameObject.Find("BFVRApp").transform.position += new Vector3(0,0.4f,0);
            buttonImage.sprite = twoSprite;
            }
        else if (state == 4)
        {
            GameObject.Find("BFVRApp").transform.position += new Vector3(0,0.6f,0);
            buttonImage.sprite = fourSprite;
        }
    }
    
    public void ToggleSit()
    {
        if (state == 4)
        {
            state = 1;
            GameObject.Find("BFVRApp").transform.position -= new Vector3(0,0.6f,0);
            buttonImage.sprite = oneSprite;
        }
        else
        {
            state++;
            GameObject.Find("BFVRApp").transform.position += new Vector3(0,0.2f,0);
            if (state == 2) buttonImage.sprite = twoSprite;
            if (state == 3) buttonImage.sprite = threeSprite;
            if (state == 4) buttonImage.sprite = fourSprite;
        }
        BFVRApp.playerHeightState = state;
    }
}
