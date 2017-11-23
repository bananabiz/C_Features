using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using GGL;

namespace MOBA
{
    public class PathFollowing : SteeringBehaviour
    {
        public Transform target;  //get to the target
        public float nodeRadius = 0.1f;  //how big each node is for the agent
        public float targetRadius = 3f;  //separate from the nodes that the agent follows
        public bool isAtTarget = false;  //has the agent reached the target node?
        public int currentNode = 0;  //keep track of the current node the agent is following

        private NavMeshAgent nav;  //Reference to the agent component
        private NavMeshPath path;  //Stores the calculated path in this variable
        
        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
        }

        // Modified seek function
        Vector3 Seek(Vector3 target)
        {
            Vector3 force = Vector3.zero;
            // Get the distance to target
            float distanceToTarget = Vector3.Distance(target, transform.position);
            // Calculate distance - Ternary Operator 
            // return value = <condition> ? <statement a> : <statement b>
            float radius = isAtTarget ? targetRadius : nodeRadius;

            // Is the magnitude greater than distance?
            if (distanceToTarget > radius)
            {
                // Apply weighting to force
                Vector3 direction = (target - transform.position).normalized * weighting;
                // Apply desired force to force (removing current owner's velocity)
                force = direction - owner.velocity;
            }
            // Return force
            return force;
        }

        void Update()
        {
            //is the path calculated?
            if (path != null)
            {
                //corners refers to the nodes that Unity generated through A*
                Vector3[] corners = path.corners;
                //has generated corners for the path?
                if (corners.Length > 0)
                {
                    //store the last corner into target pos
                    Vector3 targetPos = corners[corners.Length - 1];

                    //draw the target
                    GizmosGL.color = new Color(1, 0, 0, 0.3f);
                    GizmosGL.AddSphere(targetPos, targetRadius * 2f);

                    //calculate distance from agent to target
                    float distance = Vector3.Distance(transform.position, targetPos);
                    //is the distance greater than target radius?
                    if (distance >= targetRadius)
                    {
                        GizmosGL.color = Color.cyan;
                        for (int i = 0; i < corners.Length - 1; i++)
                        {
                            Vector3 nodeA = corners[i];
                            Vector3 nodeB = corners[i + 1];
                            GizmosGL.AddLine(nodeA, nodeB, 0.1f, 0.1f);
                            GizmosGL.AddSphere(nodeB, 1f);
                            Gizmos.color = Color.red;
                        }
                        
                    } 
                }
                
            }
        }

        public override Vector3 GetForce()
        {
            Vector3 force = Vector3.zero;

            //is there not a target?
            if (!target)
                return force;

            //calculate path using the nav agent
            if (nav.CalculatePath(target.position, path))
            {
                //is the path finished calculating?
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    Vector3[] corners = path.corners;
                    //are there any corners in the path?
                    if (corners.Length > 0)
                    {
                        int lastIndex = corners.Length - 1;
                        //is currentNode at the end of the list?
                        if (currentNode >= corners.Length)
                        {
                            currentNode = lastIndex;  //cap currentNode to lastIndex
                        }
                        //get the current corner position
                        Vector3 currentPos = corners[currentNode];
                        //get the distance to current pos
                        float distance = Vector3.Distance(transform.position, currentPos);
                        //is the distance within nodeRadius
                        if (distance <= nodeRadius)
                        {
                            //move to the next node
                            currentNode++;
                        }
                        //is the agent at the target
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);

                        isAtTarget = distanceToTarget <= targetRadius;  //return true or false --> if( ){ }else{ } 

                        //seek towards current node's position
                        force = Seek(currentPos);
                    }

                }
            }

            return force;
        }

        #region NOTES
        int SumOf(params int[] values)  //params - allows to add as many as variables in the array
        {
            int result = 0;
            foreach (var n in values)
            {
                result += n;
            }
            return result;
        }
        #endregion
    }
}