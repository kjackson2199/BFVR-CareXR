using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    public class BFVRCollider : MonoBehaviour
    {
        public string ColliderTag = "";
        protected RigidbodyConstraints _orgConstraints;
        Rigidbody body;

        private void Start()
        {
            body = GetComponent<Rigidbody>();
            if (body) _orgConstraints = body.constraints;
        }
    }
}
