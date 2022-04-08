using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureArrayAnimator : MonoBehaviour
{
    public Texture[] texList;
    int curTex = 0;


    public void PlayTexAnim()
    {
        StartCoroutine(Switch());
    }
    
    IEnumerator Switch()
    {
        yield return new WaitForEndOfFrame();//This is why we're not using a loop here.
        GetComponent<Renderer>().material.mainTexture = texList[curTex];
        if (curTex < texList.Length) StartCoroutine(Switch());
    }
}
