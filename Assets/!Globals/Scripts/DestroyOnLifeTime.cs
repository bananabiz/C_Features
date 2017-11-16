using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnLifeTime : MonoBehaviour
{
    public float lifeTime = 5f;

	// Use this for initialization
	void Start ()
    {
        //Destroy self after lifeTime
        Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
