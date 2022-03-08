using BFVR.ChapterManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BFVR.UIModule
{
    public class BFVRPopupTagManager : MonoBehaviour
    {
        public GameObject PopupTagPrefab;
        public List<Transform> tagLocations = new List<Transform>();

        const int initialTagPoolSize = 2;

        List<GameObject> popupTagPool = new List<GameObject>();
        BFVRChapterManager chMan;

        private void Start()
        {
            chMan = FindObjectOfType<BFVRChapterManager>();
            if(PopupTagPrefab)
            {
                for (int i = 0; i < initialTagPoolSize; i++)
                {
                    GameObject g = Instantiate(PopupTagPrefab);
                    g.transform.position = Vector3.zero;
                    g.transform.parent = this.gameObject.transform; // Set manager as parent
                    popupTagPool.Add(g);
                }
            }
        }

        public void DisplayTag(int locationIndex)
        {
            foreach(GameObject g in popupTagPool)
            {
                BFVRPopupTag t = g.GetComponent<BFVRPopupTag>();
                if(!t.gameObject.activeSelf)
                {
                    t.transform.position = tagLocations[locationIndex].position;
                    t.transform.localScale = tagLocations[locationIndex].localScale;
                    t.gameObject.SetActive(true);
                    break;
                }
            }
        }

        public void HideTags(GameObject tagObject = null)
        {
            foreach(GameObject g in popupTagPool)
            {
                g.SetActive(false);
            }
        }
    }
}