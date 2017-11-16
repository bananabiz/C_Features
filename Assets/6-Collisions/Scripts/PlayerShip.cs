using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collisions
{ 
 
public class PlayerShip : MonoBehaviour
    {
        public float acceleration = 30f;
        public float rotationSpeed = 180f;
        public float gottaGoFast = 100f;

        private Rigidbody2D rigid;

	    // Use this for initialization
	    void Start ()
        {
            rigid = GetComponent<Rigidbody2D>();
	    }

        /*
        void FixedUpdate()
        {
            Physics.OverlapSphereAll()
        }
        */
	
	    // Update is called once per frame
	    void Update ()
        {
            Accelerate();
            Rotate();
        }

        void Accelerate()
        {
            float inputV = Input.GetAxis("Vertical");
            float currentSpeed = acceleration;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = gottaGoFast;
            }
            rigid.AddForce(transform.up * inputV * currentSpeed);
        }

        void Rotate()
        {
            float inputH = Input.GetAxis("Horizontal");
            // Vector3.back (...from outer space!), angle
            transform.Rotate(Vector3.back, rotationSpeed * inputH * Time.deltaTime);
        }
    }
}
