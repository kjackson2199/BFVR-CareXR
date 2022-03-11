using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR
{
    public class BFVRObjectAnimation : MonoBehaviour
    {
        public Animator anim;

        void PlayAnimState(string StateName)
        {
            if (anim != null)
            {
                anim.Play(StateName);
            }
        }
    }
}
