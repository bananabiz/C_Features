using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidSpawner : MonoBehaviour
    {
        public GameObject[] asteroidPrefabs;
        public float spawnRate = 1f;
        public float spawnRadius = 5f;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }

        void Spawn()
        {
            // Generate randomized position
            Vector3 rand = Random.insideUnitSphere * spawnRadius;
            // Cancel z axis
            rand.z = 0f;
            // Offset generated position by transform's position
            Vector3 position = transform.position + rand;
            // Generate random index into prefab array
            int randIndex = Random.Range(0, asteroidPrefabs.Length);
            // Get random asteroid gameObject
            GameObject randAsteroid = asteroidPrefabs[randIndex];
            // Clone random asteroid
            GameObject clone = Instantiate(randAsteroid);
            // Set position of clone
            clone.transform.position = position;
        }

        void Start()
        {
            InvokeRepeating("Spawn", 0f, spawnRate);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
