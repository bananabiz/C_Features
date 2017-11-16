using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{
    public class Ball : MonoBehaviour
    {
        public float stopSpeed = 0.2f;

        private Rigidbody rigid;

        void Start()
        {
            rigid = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            Vector3 vel = rigid.velocity;
            // if velocity is going up on y
            if (vel.y > 0)
            {
                //cap it down to zero
                vel.y = 0;
            }
            if (vel.magnitude < stopSpeed)
            {
                //cancel out velocity
                vel = Vector3.zero;
            }
            rigid.velocity = vel;
        }

        //perform physics impact
        public void Hit(Vector3 direction, float speed)
        {
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }
        
    }
}