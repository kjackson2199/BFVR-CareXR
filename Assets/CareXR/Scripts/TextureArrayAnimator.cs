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
        yield return new WaitForEndOfFrame();
        GetComponent<Renderer>().material.mainTexture = texList[curTex];
        curTex = (curTex+1) % texList.Length; // A convenient way to loop an index
        if (curTex < texList.Length) StartCoroutine(Switch());
    }
}
