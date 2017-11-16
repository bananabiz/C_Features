using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using GGL;

namespace MOBA
{
    [RequireComponent(typeof(Camera))]
    public class AIAgentDirector : MonoBehaviour
    {
        public LayerMask hitLayers;
        public float rayDistance = 1000f;
        public AIAgent[] agentsToDirect;

        private Camera cam;
        private Transform selectionPoint;

        void Awake()
        {
            cam = GetComponent<Camera>();
        }

        void Start()
        {
            GameObject g = new GameObject("Target Location");
            selectionPoint = g.transform;
        }

        void AssignTargetToAllAgents(Transform target)
        {
            foreach (var agent in agentsToDirect)  //loop through all agents to direct
            {
                Seek s = agent.GetComponent<Seek>();
                if (s != null)  //is there a seek component on the agent?
                {
                    s.target = target;  //assign target
                }
            }
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))  // Is mouse button down?
            {
                // calculate ray from camera
                Ray camRay = cam.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                // perform raycast
                if (Physics.Raycast(camRay, out rayHit, rayDistance, hitLayers))
                {
                    NavMeshHit navHit;
                    // Perform nav mesh sampling (detects if ray is on nav mesh)
                    if (NavMesh.SamplePosition(rayHit.point, out navHit, rayDistance, -1))
                    {
                        // Set the new position to the hit one on the navmesh
                        selectionPoint.position = navHit.position;
                        // Assign the target to all the agents
                        AssignTargetToAllAgents(selectionPoint);
                    }
                }
            }
        }
    }
}
