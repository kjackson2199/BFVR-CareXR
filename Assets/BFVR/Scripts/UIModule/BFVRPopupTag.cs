using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BFVR.UIModule
{
    public class BFVRPopupTag : MonoBehaviour
    {
        public Text TagText;
        public GameObject TagPivot;

        Camera mainCamera;

        private void Start()
        {
            AnimationTextDirections.onShowNextEvent += AnimationTextDirections_onShowNextEvent;
            gameObject.SetActive(false);
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
            TagText.text = text;
        }
    }
}
