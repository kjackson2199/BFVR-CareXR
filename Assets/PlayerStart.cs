using BFVR;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour
{
    private void Start()
    {
        if(BFVRApp.Player != null)
        {
            BFVRApp.Player.transform.position = gameObject.transform.position;
            BFVRApp.Player.transform.rotation = gameObject.transform.rotation;

            DestroyImmediate(gameObject);
        }
    }

    private void Update()
    {
        if(BFVRApp.Player != null)
        {
            BFVRApp.Player.transform.position = gameObject.transform.position;
            BFVRApp.Player.transform.rotation = gameObject.transform.rotation;

            DestroyImmediate(gameObject);
        }
    }
}
