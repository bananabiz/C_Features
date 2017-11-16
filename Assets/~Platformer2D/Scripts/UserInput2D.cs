using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer2D
{
    // Forces unity to add 'Controller2D' component to 
    // the same object as 'UserInput2D'
    // you attach this script to a GameObject
    [RequireComponent(typeof(Controller2D))]
    public class UserInput2D : MonoBehaviour
    {
        // variable to store the Controller2D component
        private Controller2D controller;
        
        void Awake()
        {
            controller = GetComponent<Controller2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // Get input on the horizontal axis (A or D)
            float inputH = Input.GetAxis("Horizontal");
            controller.Move(inputH);  // Move controller using input axis

            if (Input.GetKeyDown(KeyCode.Space))  // Check if space is down
            {
                controller.Jump();  // Get player to jump
            }

        }
    }
}
