using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{
public class Paddle : MonoBehaviour {

        public float movementSpeed = 20f;
        public Ball currentBall;
        public Vector2[] directions = new Vector2[]
            {
                new Vector2(-0.5f, 0.5f),
                new Vector2(0.5f, 0.5f)
            };
        public GameObject paddle;

        private bool timer = false;

        //private Vector3 posi = Paddle.Transform.position;

	// Use this for initialization
	void Start ()
    {
            Vector3 posi = paddle.transform.position;
            //Grab currentBall form children of Paddle
            currentBall = GetComponentInChildren<Ball>();
	}

        void Fire()
        {
            //Detach the ball from children
            currentBall.transform.SetParent(null); // ... I'm Batman
            //Select a random direction from array
            Vector3 randomDir = directions[Random.Range(0, directions.Length)];
            //Fire off ball in random direction
            currentBall.Fire(randomDir);
        }

        void CheckInput()
        {
            if (timer == true)
            {
                return;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
                timer = true;
            }

        }

        void Movement()
        {
            // Get input on the horizontal axis
            float inputH = Input.GetAxis("Horizontal");
            // Add a force (inputH to determine which direction)
            Vector3 force = transform.right * inputH;
            // Amplify force by movementSpeed 
            force *= movementSpeed;
            // Multiply by deltaTime for smoothness
            force *= Time.deltaTime;
            // Add force to position
            transform.position += force;
        }

	// Update is called once per frame
	void Update ()
        {
            CheckInput();
            Movement();
	    }
    }
}
