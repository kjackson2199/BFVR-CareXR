using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BFVR.Interactable
{
    /// <summary>
    /// BFVR Hand Raycaster. Cast rays from palm to allow grabbing grabbable objects. Attach to palm empty gameobject or socket.
    /// </summary>
    /// 
    public class BFVRHandRaycaster : MonoBehaviour
    {
        const int rayCount = 300;
        const int behindHandRayCount = 300;

        [Range(.1f, .5f)] public float maxRayDistance = .25f;
        [HideInInspector][Range(-4, -1)] public float grabSpreadOffset = -2;
        [Range(.01f, 1)] public float radius = .1f;

        public LayerMask GrabbableLayers;

        Vector3[] handRays;

        private void Start()
        {
            //PrecalculatePalmRays();
        }

        public void RaycastSphere(out RaycastHit outHit)
        {
            Physics.SphereCast(gameObject.transform.position, radius, gameObject.transform.forward, out outHit, maxRayDistance, GrabbableLayers);
        }

        private void PrecalculatePalmRays()
        {
            List<Vector3> rays = new List<Vector3>();
            List<Vector3> rearRays = new List<Vector3>();


            for (int i = 0; i < rayCount; i++)
            {
                float ampI = Mathf.Pow(i, 1.05f + grabSpreadOffset / 10f) / (Mathf.PI * 0.8f);
                rays.Add(Quaternion.Euler(0, Mathf.Cos(i) * ampI + 90, Mathf.Sin(i) * ampI) * -Vector3.right);
            }

            for (int i = 0; i<behindHandRayCount; i++)
            {
                float ampI = Mathf.Pow(i, 1.05f + grabSpreadOffset / 10f) / (Mathf.PI * 0.8f);
                rays.Add((Quaternion.Euler(0, Mathf.Cos(i) * ampI + 90, Mathf.Sin(i) * ampI) * Vector3.right) - (Vector3.right * -.1f));
            }

            handRays = rays.ToArray();
        }

        /// <summary>
        /// Cast rays and returns average hit vector.
        /// </summary>
        /// <param name="closestHit">Raycast closest to hit object.</param>
        public Vector3 RaycastClosestHit(out RaycastHit closestHit)
        {
            List<RaycastHit> hits = new List<RaycastHit>();
            foreach(var ray in handRays)
            {
                RaycastHit hit;
                if (Physics.Raycast(gameObject.transform.position, gameObject.transform.rotation * ray, out hit, maxRayDistance, GrabbableLayers))
                {
                    hits.Add(hit);
                }

                Debug.DrawRay(gameObject.transform.position, gameObject.transform.rotation * ray, Color.red, 10);
            }
            if (hits.Count > 0)
            {
                closestHit = hits[0];
                var closestHitIndex = 0;
                var minMax = new Vector2(1f, 1.05f);
                Vector3 dir = Vector3.zero;
                for(int i = 0; i < hits.Count; i++)
                {
                    var closestMulti = Mathf.Lerp(minMax.x, minMax.y, ((float)closestHitIndex) / hits.Count);
                    var multi = Mathf.Lerp(minMax.x, minMax.y, ((float)i) / hits.Count);
                    if(hits[i].distance*multi < closestHit.distance*closestMulti)
                    {
                        closestHit = hits[i];
                        closestHitIndex = i;
                    }
                    dir += hits[i].point - gameObject.transform.position;
                }
                return dir / hits.Count;
            }

            closestHit = new RaycastHit();
            return Vector3.zero;
        }
    }
}
