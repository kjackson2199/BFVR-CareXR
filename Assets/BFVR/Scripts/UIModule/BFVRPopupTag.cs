using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BFVR.UIModule
{
    public class BFVRPopupTag : MonoBehaviour
    {
        public Text text;
        public GameObject TagPivot;

        string tagText = "";
        Camera mainCamera;

        private void Start()
        {
            gameObject.SetActive(false);
            AnimationTextDirections.onShowNextEvent += AnimationTextDirections_onShowNextEvent;
        }

        private void Update()
        {
            if (!mainCamera)
            {
                GameObject playerCameraObject = GameObject.FindGameObjectWithTag("MainCamera");
                if (!playerCameraObject)
                    return;
                mainCamera = playerCameraObject.GetComponent<Camera>();
                if (!mainCamera)
                    return;
            }
            TagPivot.transform.LookAt(mainCamera.gameObject.transform);
        }

        private void OnEnable()
        {            
            text.text = tagText;
        }

        private void OnDestroy()
        {
            AnimationTextDirections.onShowNextEvent -= AnimationTextDirections_onShowNextEvent;
        }

        void AnimationTextDirections_onShowNextEvent(string text)
        {
            // Set new text from event
            SetTagText(text);
        }

        public void SetTagText(string text)
        {
            tagText = text;
        }
    }
}
