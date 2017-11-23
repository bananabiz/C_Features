using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using GGL;

namespace MOBA
{
    [RequireComponent(typeof(Camera))]
    public class AIAgentPatrolSelector : MonoBehaviour
    {
        public LayerMask hitLayers;
        public float rayDistance = 1000f;
        public AIAgent[] agentsToDirect;
        public List<Transform> patrolPoints;

        private Camera cam;

        // Use this for initialization
        void Start()
        {
            cam = GetComponent<Camera>();
            // Loop through each agent and assign patrol selector to all agents
            foreach (var agent in agentsToDirect)
            {
                Patrol p = agent.GetComponent<Patrol>();
                if (p != null)
                {
                    // Give patrol reference to this script
                    p.patrolSelector = this;
                }
            }
        }

        void AddPatrolPoint(Vector3 point)
        {
            GameObject g = new GameObject("Point " + patrolPoints.Count);
            g.transform.position = point;
            patrolPoints.Add(g.transform);
        }

        void HandleSelector()
        {
            // Is the right mouse down?
            if (Input.GetMouseButtonDown(1))
            {
                // Generate the ray
                Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                // Perform raycast
                if (Physics.Raycast(camRay, out rayHit, rayDistance, hitLayers))
                {
                    NavMeshHit navHit;
                    // Is raycast point on the NavMesh?
                    if (NavMesh.SamplePosition(rayHit.point, out navHit, rayDistance, -1))
                    {
                        // Adds a new patrol point
                        AddPatrolPoint(rayHit.point);
                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            HandleSelector();

            //draw each point
            foreach (var p in patrolPoints)
            {
                GizmosGL.AddSphere(p.position, 1f); 
            }
        }
    }
}
