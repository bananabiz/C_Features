using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Cue : MonoBehaviour
    {
        public Ball targetBall; //target ball selected which is generally the Cue ball)
        public float minPower = 0f; //the min power which maps to the distance
        public float maxPower = 20f; //the max power which maps to the distance
        public float maxDistance = 5f; // the maximum distance in units the cue can be dragged back

        private float hitPower; // the final calculation hit power to fire the ball
        private Vector3 aimDirection; //the aim direction the ball should fire
        private Vector3 prevMousePos; //the mouse position obtained when left-clicking
        private Ray mouseRay; //the ray of the mouse

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(mouseRay.origin, mouseRay.origin + mouseRay.direction * 1000f);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(targetBall.transform.position, targetBall.transform.position + aimDirection * hitPower);

        }

        // Rotates the cue to wherever the mouse is pointing (using Raycast)
        void Aim()
        {
            //calculate mouse ray before performing raycast
            mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //raycast hit container for the hit information
            RaycastHit hit;
            //perform the raycast
            if (Physics.Raycast(mouseRay, out hit))
            {
                //Obtain direction from the cue's position to the raycast's hit point
                Vector3 dir = transform.position - hit.point;
                //convert direction to angle in degrees
                float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
                //rotate towards that angle
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
                //position cue to the ball's position
                transform.position = targetBall.transform.position;

            }
        }

        //Deactivates the Cue
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        //Activates the Cue
        public void Activate()
        {
            Aim();
            gameObject.SetActive(true);
        }

        //Allows you to drag the cue back and calculated power dealt to the ball
        void Drag()
        {
            //store target ball's position in smaller variable
            Vector3 targetPos = targetBall.transform.position;
            //get mouse position in world units
            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = Vector3.Distance(prevMousePos, currMousePos);

            distance = Mathf.Clamp(distance, 0, maxDistance);

            float distPercentage = distance / maxDistance;

            hitPower = Mathf.Lerp(minPower, maxPower, distPercentage);

            transform.position = targetPos - transform.forward * distance;

            aimDirection = (targetPos - transform.position).normalized;

        }

        void Fire()
        {

            targetBall.Hit(aimDirection, hitPower);

            Deactivate();
        }



        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {

                prevMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            }
            if (Input.GetMouseButton(0))
            {
                Drag();
            }
            else
            {

                Aim();
            }
            if (Input.GetMouseButtonUp(0)) 
            {
                Fire();
            }
        }
    }
}
