using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Recursion
{ 
public class RecursiveSpawner : MonoBehaviour
    {
        public GameObject spawnPrefab;
        public int amount = 100;
        public float positionOffset = 5f;
        [Range(0, 1)]
        public float percentageReduction = 0.2f;

        void RecursiveSpawn(int currentAmount,
                            Vector3 position,
                            Vector3 scale)
        {
            Vector3 adjustedScale = scale * (1 - percentageReduction);
            //Vector3 adjustedPosition = position + Vector3.up * positionOffset;
            Vector3 adjustedPosition = position + Vector3.up * (adjustedScale.magnitude * positionOffset);

            //Instantiate SpawnPrefab
            GameObject clone = Instantiate(spawnPrefab);
            clone.transform.position = adjustedPosition;
            clone.transform.localScale = adjustedScale;

            //Decrement
            amount--;

            if (amount <= 0)
            {
                return;
            }

            RecursiveSpawn(amount, adjustedPosition, adjustedScale); 
        }

    	// Use this for initialization
    	void Start ()
        {
            Vector3 position = transform.position;
            Vector3 scale = spawnPrefab.transform.localScale;
            RecursiveSpawn(amount, position, scale);
    	}
    	
    }
}
