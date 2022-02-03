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
        public enum CursorState
        {
            Hidden,
            Normal,
            Hovering
        }

        private CursorState cursorState = CursorState.Normal;

        public LayerMask cursorBlockingMask;

        [Space] public Color CursorNormalColor;
        public Sprite NormalCursorSprite;

        [Space] public Color CursorHoverOverTargetColor;
        public Sprite CursorHoverSprite;

        [Space] public Color CursorClickColor;
        public Sprite CursorClickSprite;

        [Space] public float FadeDuration = .1f;

        private void Update()
        {
            RaycastHit hit = Raycast();
            if(hit.collider)
            {
                BFVR3DCursorInputModule.SetTargetObject(hit.collider.gameObject);
                SetCursorState(CursorState.Hovering);
            }
            else
            {
                BFVR3DCursorInputModule.SetTargetObject(null);
                SetCursorState(CursorState.Normal);
            }
        }

        private void OnEnable()
        {
            SetCursorState(CursorState.Normal);
        }

        private void OnDisable()
        {
            SetCursorPosition(new Vector3(0, 0, 0));
            SetCursorState(CursorState.Hidden);
            ResetCursor();
            BFVR3DCursorInputModule.SetTargetObject(null);
        }

        void SetCursorState(CursorState newState)
        {
            cursorState = newState;

            switch(cursorState)
            {
                case CursorState.Hovering:
                    StartCoroutine(FadeCursorColor(CursorNormalColor, CursorHoverOverTargetColor, FadeDuration));
                    SetCursorSprite(CursorHoverSprite);
                    break;
                case CursorState.Normal:
                    StartCoroutine(FadeCursorColor(CursorHoverOverTargetColor, CursorNormalColor, FadeDuration));
                    SetCursorSprite(NormalCursorSprite);
                    break;
                default:
                    SetCursorColor(CursorNormalColor);
                    SetCursorSprite(NormalCursorSprite);
                    break;
            }
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

        public void ResetCursor()
        {
            SetCursorPosition(new Vector3(0, 0, 0));
            SetCursorFacingNormal(Vector3.up);
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

        void SetCursorSprite(Sprite sprite)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = sprite;
        }

        void SetCursorColor(Color color)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.color = color;
        }

        IEnumerator FadeCursorColor(Color a, Color b, float duration)
        {
            float timeElapsed = 0;
            SpriteRenderer cursorSprite = GetComponent<SpriteRenderer>();

            while(timeElapsed <= duration)
            {
                cursorSprite.color = Color.Lerp(b, a, timeElapsed / duration);
                timeElapsed += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}