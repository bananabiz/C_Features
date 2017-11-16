using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Classes
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class RotateToVelocity : MonoBehaviour
    {
        private Rigidbody2D rigid2D;

        // Use this for initialization
        void Start()
        {
            rigid2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 vel = rigid2D.velocity;
            float angle = Mathf.Atan2(vel.y, vel.x);
            rigid2D.rotation = angle * Mathf.Rad2Deg;
        }
    }
}