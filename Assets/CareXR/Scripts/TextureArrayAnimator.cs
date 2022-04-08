using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureArrayAnimator : MonoBehaviour
{
    public Texture[] texAnim1;
    public Texture[] texAnim2;
    public Texture[] texAnim3;
    public Texture[] texAnim4;
    public Texture[] texAnim5;
    int curTex = 0;
    int targetAnim = 1;
    public int targetMat = 0;

    //void Start()
    //{
    //    //Add stuff here for testing
    //    PlayTexAnim(1);
    //}

    public void PlayTexAnim(int animationNumber)
    {
        targetAnim = animationNumber;
        curTex = 0;
        StartCoroutine(Switch());
    }
    
    IEnumerator Switch()//This amazing script was made by Blake Martin using the help of Unity forum users and outdated Unity Documentation.
    {
        yield return new WaitForEndOfFrame();//This is why we're using a coroutine instead of a loop.
        if (targetAnim == 1)
        {
            Debug.Log(curTex);
            GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", texAnim1[curTex]);
            curTex++;
            if (curTex < texAnim1.Length) StartCoroutine(Switch());
        }
        else if (targetAnim == 2)
        {
            GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", texAnim2[curTex]);
            curTex++;
            if (curTex < texAnim2.Length) StartCoroutine(Switch());
        }
        else if (targetAnim == 3)
        {
            GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", texAnim3[curTex]);
            curTex++;
            if (curTex < texAnim3.Length) StartCoroutine(Switch());
        }
        else if (targetAnim == 4)
        {
            GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", texAnim4[curTex]);
            curTex++;
            if (curTex < texAnim4.Length) StartCoroutine(Switch());
        }
        else if (targetAnim == 5)
        {
            GetComponent<Renderer>().materials[targetMat].SetTexture("_MainTex", texAnim5[curTex]);
            curTex++;
            if (curTex < texAnim5.Length) StartCoroutine(Switch());
        }
    }
}
