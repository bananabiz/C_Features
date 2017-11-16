using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(PlayerController))]
    public class Mario : MonoBehaviour
    {
        public float rayDistance;

        private PlayerController pC;
        private Ray stompRay;


        void OnDrawGizmos()
        {
            RecalculateRay();
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(stompRay.origin, stompRay.origin + stompRay.direction * rayDistance); 
        }

        void Start()
        {
            pC = GetComponent<PlayerController>(); 
        }

        // Update is called once per frame
        void Update()
        {
            if (!pC.isGrounded)
            {
                CheckStomp();
            }
        }

        void RecalculateRay()
        {
            stompRay = new Ray(transform.position, Vector3.down);
        }

        void CheckStomp()
        {
            // perform raycast
            RaycastHit hit;
            if (Physics.Raycast(stompRay, out hit, rayDistance))
            {
                // are we on top of enemy
                Enemy e = hit.collider.GetComponent<Enemy>();
                if (e != null)
                {
                    // add force up
                    e.Damage();
                    pC.Jump(true);
                }
            }
        }
    }
}
