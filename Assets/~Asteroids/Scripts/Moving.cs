using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Moving : MonoBehaviour
    {
        public float accelerate = 10f;
        public float rotationSpeed = 360f;

        private Rigidbody2D rigid; 

        // Use this for initialization
        void Awake()
        {
            rigid = GetComponent<Rigidbody2D>(); 
        }

        void Accelerate()
        {
            float inputV = Input.GetAxis("Vertical");
            rigid.AddForce(transform.up * inputV * accelerate);
        }

        void Rotate()
        {
            float inputH = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.back, rotationSpeed * inputH * Time.deltaTime);
        }
        
        // Update is called once per frame
        void Update()
        {
            Accelerate();
            Rotate();
        }
    }
}
