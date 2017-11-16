using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Billiards
{ 
    public class CollisionPockets : MonoBehaviour
    {
        public GameObject pocket;
        
        public Rigidbody ball;
    
    	// Use this for initialization
    	void Start () {
            ball = GetComponent<Rigidbody>();
    	}

        private void OnCollisionEnter(Collision ball)
        {
            if (ball.gameObject.tag == "ball")
            {
                Destroy(ball.gameObject, 0.1f);
            }
        }
 
        // Update is called once per frame
        void Update () {
    		
    	}
    }
}
