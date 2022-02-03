using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.InputModule
{
    /// <summary>
    /// Standard BFVR Cursor. Handles cursor transforms and graphics raycasting for UI elements.
    /// </summary>
    /// 

    public class BFVRCursor : MonoBehaviour
    {
        public LayerMask cursorBlockingMask;

        [Space] public Color CursorNormalColor;
        public Sprite NormalCursorSprite;

        [Space] public Color CursorHoverOverTargerColor;
        public Sprite CursorHoverSprite;

        [Space] public Color CursorClickColor;
        public Sprite CursorClickSprite;

        [Space] public float FadeDuration = .1f;

        private void Update()
        {
            RaycastHit hit = Raycast();
            if(hit.collider)
            {
                BFVR3DCursorInput.SetTargetObject(hit.collider.gameObject);
            }
            else
            {
                BFVR3DCursorInput.SetTargetObject(null);
            }
        }

        private void OnDisable()
        {
            SetCursorPosition(new Vector3(0, 0, 0));
            BFVR3DCursorInput.SetTargetObject(null);
        }

        public void SetCursorScale(float scale)
        {
            gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }
        public void SetCursorFacingNormal(Vector3 normalDir) 
        {
            gameObject.transform.forward = normalDir;
        }
        public void SetCursorPosition(Vector3 pos) 
        {
            gameObject.transform.position = pos;
        }

        RaycastHit Raycast()
        {
            Ray r = new Ray();

            r.origin = gameObject.transform.position;
            r.direction = gameObject.transform.forward * -1;

            RaycastHit hitInfo;
            Physics.Raycast(r, out hitInfo, 1, cursorBlockingMask);

            return hitInfo;
        }

        IEnumerator FadeCursorColor(Color a, Color b, float duration)
        {
            float timeElapsed = 0;
            Color newColor = Color.black;

            while(timeElapsed < duration)
            {
                newColor = Color.Lerp(a, b, timeElapsed);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}