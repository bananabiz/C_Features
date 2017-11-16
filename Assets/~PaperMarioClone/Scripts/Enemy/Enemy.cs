using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    public class Enemy : MonoBehaviour
    {
        public float movementSpeed = 10f;
        public float rayDistance = 1f;
        public bool isMovingLeft = false;

        private Ray leftRay;
        private Ray rightRay;
        private BoxCollider box;

        private void Awake()
        {
            box = GetComponent<BoxCollider>(); 
        }

        public void OnDrawGizmos()
        {
            box = GetComponent<BoxCollider>(); 
            //OnGizmos runs in Editor, Awake runs in game, so need to GetComponent again
            RecalculateRays();
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftRay.origin, leftRay.origin + leftRay.direction * rayDistance);
            Gizmos.DrawLine(rightRay.origin, rightRay.origin + rightRay.direction * rayDistance);
        }

        void RecalculateRays()
        {
            Vector3 halfSize = box.bounds.size * 0.5f;
            Vector3 leftPos = transform.position - Vector3.left * halfSize.x;
            Vector3 rightPos = transform.position - Vector3.right * halfSize.x;
            // if Raycast happen inside the collider it will not work 
            leftRay = new Ray(leftPos, Vector3.down);
            rightRay = new Ray(rightPos, Vector3.down);
        }

        public void Move()
        {
            Vector3 pos = transform.position;

            // perform raycast check
            bool isLeftHitting = Physics.Raycast(leftRay, rayDistance);
            bool isRightHitting = Physics.Raycast(rightRay, rayDistance);

            // is the player close to left edge
            if (isLeftHitting && !isRightHitting)
                isMovingLeft = false;  //move right
            // is the player close to right edge
            else if (isRightHitting && !isLeftHitting)
                isMovingLeft = true;  //move left

            Vector3 dir = Vector3.zero;
            if (isMovingLeft)  //is the player moving left
                dir += Vector3.left;  //move left
            else  //is the player moving right
                dir += Vector3.right;  //move right

            //apply movement speed and deltaTime
            pos += dir * movementSpeed * Time.deltaTime;
            //set the position to newly modified pos
            transform.position = pos;
        }

        //default update for all Enemies
        public virtual void Update()
        {
            Move();
        }

        public virtual void Damage(int combo = 0)
        {


        }
    }
}
