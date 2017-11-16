using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class AIAgent : MonoBehaviour
    {
        public Vector3 force; // total force calculated from behaviours
        public Vector3 velocity; // direction of travel and speed
        public float maxSpeed = 100f; // max amount of velocity
        public float maxDistance = 10f; 
        public bool freezeRotation = false; // do we want to freeze rotation?

        private NavMeshAgent nav;
        // List of SteeringBehaviours (i.e, Seek, Flee, Wander, etc)
        private List<SteeringBehaviour> behaviours;
        // sample: SteeringBehaviour b = behaviours[i]
        

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            behaviours = new List<SteeringBehaviour>(GetComponents<SteeringBehaviour>());
        }
        
        void ComputeForces()
        {
            // Set force = Vector3.zero
            force = Vector3.zero;
            // For i := 0 < behaviours.Count
            for (int i = 0; i < behaviours.Count; i++)
            {
                // Let behaviour = behaviours[i]
                SteeringBehaviour behaviour = behaviours[i];
                // IF behaviour.isActiveAndEnabled == false
                if (behaviour.isActiveAndEnabled == false)
                {
                    // continue
                    continue;
                }
                // Set force = force + behaviour.GetForce() * behaviour.weight
                force += behaviour.GetForce();  // * behaviour.weight;
                // IF force > maxVelocity
                if (force.magnitude > maxSpeed)
                {
                    // Set force = force.normalized * maxSpeed
                    force = force.normalized * maxSpeed;
                    // break
                    break;
                }
            }
        }

        void ApplyVelocity()
        {
            // Set velocity = velocity + force * deltaTime
            velocity += force * Time.deltaTime;
            // IF velocity.magnitude > maxSpeed
            if (velocity.magnitude > maxSpeed)
            {
                // Set velocity = velocity.normalized * maxSpeed
                velocity = velocity.normalized * maxSpeed;
            }
            // IF velocity.magnitude > 0
            if (velocity.magnitude > 0)
            {
                // Set transform.position = transform.position + velocity * deltaTime
                transform.position += velocity * Time.deltaTime;
                // Set transform.rotation = Quaternion LookRotation (velocity)
                transform.rotation = Quaternion.LookRotation(velocity);
            }
        }
        
        // Update is called once per frame
        void FixedUpdate()
        {
            ComputeForces();
            ApplyVelocity();
        }
    }
}