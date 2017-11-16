using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Arrays
{ 
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public Vector2 direction;
        
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
	}
    }
}
