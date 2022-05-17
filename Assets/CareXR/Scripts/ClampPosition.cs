using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ClampPosition : MonoBehaviour
{
    void Update()
    {
        this.gameObject.transform.localPosition = new Vector3(this.gameObject.transform.localPosition.x,0,this.gameObject.transform.localPosition.z);//Rotating hand models makes their position fly off into the hundreds of units for some reason. This prevents that.
    }
}
