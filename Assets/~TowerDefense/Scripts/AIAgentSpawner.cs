using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class AIAgentSpawner : MonoBehaviour
    {
        public GameObject aiAgentPrefab;
        public Transform target; //Target that each AI Agent should travel to
        public float spawnRate = 1f;
        public float spawnRadius = 1f;

        // Visualization code
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            //Draw a sphere to indicate the spawn radius
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        void Spawn()
        {
            //Let clone = new instance of aiAgentPrefab;
            GameObject clone = Instantiate(aiAgentPrefab);
            //Let rand = Random Inside Unit Sphere
            Vector3 rand = Random.insideUnitSphere;
            //Set rand.y = 0
            rand.y = 0;
            //Set clone's position to transform's position + rand x spawnRadius
            clone.transform.position = transform.position + rand * spawnRadius;
            //Set aiAgent = clone's AIAgent component
            AIAgent aiAgent = clone.GetComponent<AIAgent>();
            //Set aiAgent.target = target
            aiAgent.target = target;
        }

        // Use this for initialization
        void Start()
        {
            //InvokeRepeating(functionName, time, repeatRate)
            //functionName = name of the function you want to call in the class
            //time = delay for when the function gets called the first time
            //repeatRate = how ofter the function gets called
            InvokeRepeating("Spawn", 0, spawnRate);
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
