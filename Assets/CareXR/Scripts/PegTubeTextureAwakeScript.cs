using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegTubeTextureAwakeScript : MonoBehaviour
{
    public Texture startTexture;
    public int targetMat = 0;

    void Awake()
    {
        SetStartTexture();
    }
    public void SetStartTexture()
    {
        GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", startTexture);
    }
}
