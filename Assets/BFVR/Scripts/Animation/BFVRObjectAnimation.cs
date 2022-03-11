using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR
{
    public class BFVRObjectAnimation : MonoBehaviour
    {
        public Animator anim;

        public void PlayAnimState(string StateName)
        {
            if (anim != null)
            {
                anim.Play(StateName);
            }
        }

        public void SetState(string StateName)
        {
            if(anim != null)
            {
                anim.Play(StateName, -1, 1);
            }
        }
    }
}
