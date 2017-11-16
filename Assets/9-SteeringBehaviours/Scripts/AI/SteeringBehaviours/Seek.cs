using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GGL;

namespace AI
{
    public class Seek : SteeringBehaviour
    {
        public Transform target;
        public float stoppingDistance = 1f;

        public override Vector3 GetForce()
        {
            // Let force = Vector3.zero
            Vector3 force = Vector3.zero;
            // IF target == null
            if (target == null)
            {
                return force;
            }
            // Let desiredForce = target position - transform position
            Vector3 desiredForce = target.position - transform.position;
            // IF desiredForce.magnitude > stoppingDistance
            if (desiredForce.magnitude > stoppingDistance)
            {
                // Set desiredForce = desiredForce.normalized * weight
                desiredForce = desiredForce.normalized * weight;
                // Set force = desiredForce - owner.velocity
                force = desiredForce - owner.velocity;
            }

            #region GizmosGL
            GizmosGL.color = Color.red;
            GizmosGL.AddLine(transform.position, transform.position + force, 0.1f);
            GizmosGL.color = Color.white;
            GizmosGL.AddLine(transform.position, transform.position + desiredForce, 0.1f, 0.1f);
            #endregion

            // Return the force
            return force;
        }
    }
}