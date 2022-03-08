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

        GameObject popupTagObject;
        BFVRChapterManager chMan;

        private void Start()
        {
            chMan = FindObjectOfType<BFVRChapterManager>();
            if(PopupTagPrefab)
            {
                popupTagObject = Instantiate(PopupTagPrefab);
            }
        }

        public void DisplayTag(int locationIndex)
        {
            HideTags();

            popupTagObject.transform.position = tagLocations[locationIndex].position;
            popupTagObject.transform.localScale = tagLocations[locationIndex].localScale;
            popupTagObject.gameObject.SetActive(true);
        }

        public void HideTags()
        {
            popupTagObject.SetActive(false);
        }
    }
}