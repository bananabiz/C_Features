using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LoopsArray
{

    public class Loops : MonoBehaviour
    {
        public GameObject[] spawnPrefabs;
        public float frequency = 3;
        public float amplitude = 6;
        public int spawnAmount = 10;
        public float spawnRadius = 5f;
        public string message = "Print This";
        public float printTime = 2f;

        private float timer = 0;

        void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        // Use this for initialization
        void Start()
        {
            /* whilie (condition)
            {
                // Statement
            } 
            */
            SpawnObjectsWithSine();
        }

        // Update is called once per frame
        void Update()
        {
            // Count up timer in seconds

            // Loop through until timer gets to printTime
            while (timer <= printTime) // Stick around
            {
                // Count up timer in seconds
                timer += Time.deltaTime;
                print("PUT THE COOKIE DOWN!");
                // break;
            }
        }

        void SpawnObjectsWithSine()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawned new GameObject
                int randomIndex = Random.Range(0, spawnPrefabs.Length);
                // Store randomly selected prefab
                GameObject randomPrefab = spawnPrefabs[randomIndex];
                // Instantiate randomly selected prefab
                GameObject clone = Instantiate(randomPrefab);
                // Grab the MeshRenderer
                MeshRenderer rend = clone.GetComponent<MeshRenderer>();
                // Change the color
                float r = Random.Range(0, 2);
                float g = Random.Range(0, 2);
                float b = Random.Range(0, 2);
                float alpha = 1;
                rend.material.color = new Color(r, g, b, alpha);
                // Generateed random position within circle (spehere actually)
                float x = Mathf.Sin(i * frequency) * amplitude;
                float y = i;
                float z = Mathf.Cos(i * frequency) * amplitude;
                Vector3 randomPos = transform.position + new Vector3(x, y, z);
                // Cancel out the Z
                // randomPos.z = 0;
                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        }
        void SpawnObjects()
        {
            /*
            for (initialization; condition; interation)
            {
                //Statement(s)        
            }
            */

            for (int i = 0; i < spawnAmount; i++)
            {
                // Spawned new GameObject
                GameObject clone = Instantiate(spawnPrefabs[0]);
                // Generateed random position within circle (spehere actually)
                Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
                // Cancel out the Z
                randomPos.z = 0;
                // Set spawned object's position
                clone.transform.position = randomPos;
            }
        }
    }
}