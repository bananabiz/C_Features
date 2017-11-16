using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Breakout
{ 
public class Ball : MonoBehaviour {

        public float speed = 10f; //Speed that the ball travels
        public ScoreManager scoreManager;
        public AudioSource sound;

        private Vector3 velocity; //Velocity of the ball (Direction X Speed)
        
        //Sends the ball flying in a given direction
        public void Fire(Vector3 direction)
        {
        velocity = direction* speed;
            //Vector3.Distance()
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            //Grab the contact point of the collision
            ContactPoint2D contact = other.contacts[0];
            //Calculate the reflect vector
            Vector3 reflect = Vector3.Reflect(velocity, contact.normal);
            //Calculating new veloctiy (replacing the direction but not speed)
            velocity = reflect.normalized * velocity.magnitude;
            if (other.gameObject.tag == "BlockA" || other.gameObject.tag == "BlockB" || other.gameObject.tag == "BlockC")
            {
                sound.Play();
                Destroy(other.gameObject);
                scoreManager.score++;
            }
        }


	    // Use this for initialization
	    void Start () {
            scoreManager = GameObject.Find("Score").GetComponent<ScoreManager>();
            sound = GetComponent<AudioSource>(); 
	    }
	    
	    // Update is called once per frame
	    void Update ()
        {
            //Move ball using velocity and deltaTime
            transform.position += velocity * Time.deltaTime;
	    }
    }
}