using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefense
{ 
    [RequireComponent(typeof(NavMeshAgent))]
public class AIAgent : MonoBehaviour
    {
        public Transform target;

        private NavMeshAgent nav;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();

        }

	    // Use this for initialization
	    void Start ()
        {
            //if target is not null
            if (target != null)
            {
                //set nav destination to target's position
                nav.SetDestination(target.position);
            }
	    }
	    
	    // Update is called once per frame
	    void Update () {
	    	
	    }
        }
}
