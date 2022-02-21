using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    public class SetTransform : MonoBehaviour
    {

        public List<Transform> Targets = new List<Transform>();
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetFromList(int index)
        {
            if (Targets.Count > index)
            {
                gameObject.transform.position = Targets[index].position;
                gameObject.transform.rotation = Targets[index].rotation;
            }
        }
    }
}
