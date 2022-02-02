using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.InputModule
{
    public class BFVRCursor : MonoBehaviour
    {
        public virtual void SetCursorScale(float scale)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
        public virtual void SetCursorFacingNormal(Vector3 normalDir) 
        {
            gameObject.transform.forward = normalDir;
        }
        public virtual void SetCursorPosition(Vector3 pos) 
        {
            gameObject.transform.position = pos;
        }
    }
}