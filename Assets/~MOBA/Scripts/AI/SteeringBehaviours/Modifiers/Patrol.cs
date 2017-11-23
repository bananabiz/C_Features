using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    [RequireComponent(typeof(PathFollowing))]
    public class Patrol : MonoBehaviour
    {
        public AIAgentPatrolSelector patrolSelector;  // The selector that is guiding the agent

        private int currentPoint = 0;  // The current patrol point the agent is PathFollowing to
        private PathFollowing pathFollowing;  // Reference to the attached PathFollowing script
        private List<Transform> patrolPoints;  // List of patrol point (referring to the one in the patrolSelector)

        void Start()
        {
            pathFollowing = GetComponent<PathFollowing>();
        }
        
        void Update()
        {
            // Is there currently a patrol selector?
            if (patrolSelector != null)
            {
                patrolPoints = patrolSelector.patrolPoints;  // Grab patrol points list from selector

                if (patrolPoints.Count > 0)  // Is there any patrol points added from the selector?
                {
                    if (pathFollowing.isAtTarget)  // Is the agent at the target?
                    {
                        pathFollowing.currentNode = 0;  // Reset the currentNode the agent seeks to
                        currentPoint++;  // Move to next patrol point
                    }

                    if (currentPoint >= patrolPoints.Count)  // Is currentPoint outside of list count?
                    {
                        currentPoint = 0;  // Loop back at start
                    }

                    Transform point = patrolPoints[currentPoint];
                    pathFollowing.target = point;
                }
            }
        }
    }
}