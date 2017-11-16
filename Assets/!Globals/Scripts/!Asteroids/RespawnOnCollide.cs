using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Respawn))]
public class RespawnOnCollide : MonoBehaviour
{
    public string colliderTag;

    private Respawn res;
 
	void Awake()
    {
        res = GetComponent<Respawn>();
	}
	
	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.tag == colliderTag)
        {
            res.Spawn();
        }
	}
}
