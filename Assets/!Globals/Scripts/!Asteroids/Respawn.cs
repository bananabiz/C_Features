using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public float respawnTime = 3f;

    private Vector3 spawnPos;
    private Renderer rend;
    
	void Awake ()
    {
        rend = GetComponent<Renderer>();
	}

    void Start()
    {
        spawnPos = transform.position;  //recording start position
    }

    public void Spawn()
    {
        // Start SpawnDelay coroutine
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        rend.enabled = false;  //disable renderer
        yield return new WaitForSeconds(respawnTime);  //wait for respawnTime (seconds)
        transform.position = spawnPos;  //reset position to spawnPos
        rend.enabled = true;  //enable renderer
    }

	// Update is called once per frame
	void Update ()
    {
        Spawn();
	}
}
