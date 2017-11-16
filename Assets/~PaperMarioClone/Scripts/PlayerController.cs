using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed = 10f;
        public float runSpeed = 20f;
        public float jumpHeight = 10f;
        public bool isRunning = false;
        public bool isGrounded
        {
            get { return controller.isGrounded; }
        }

        private CharacterController controller;
        private Vector3 gravity;
        private Vector3 movement;
        private bool jump = false;
        private bool jumpInstant = false;  
        private Vector3 inputDir;
        
        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        
        void Update()
        {
            if (isRunning)  //is the controller running?
                movement *= runSpeed;  //run
            else
                movement *= walkSpeed;  //walk

            if (isGrounded)  //is the controller grounded?
            {
                gravity = Vector3.zero;   //cancel out gravity only if you're grounded
                if (jump)  //is controller jumping?
                {
                    gravity.y = jumpHeight;  //make character jump
                    jump = false;
                }
            }
            else
            {
                gravity += Physics.gravity * Time.deltaTime;
            }

            if (jumpInstant)
            {
                gravity.y = jumpHeight;
                jumpInstant = false;
            }

            //apply movement
            movement += gravity;
            controller.Move(movement * Time.deltaTime);
        }

        public void Jump(bool instant = false)
        {
            if (instant)
                jumpInstant = true;
            else
                jump = true;  //jump
        }

        public void Move(float inputH, float inputV)
        {
            inputDir = new Vector3(inputH, 0, inputV);
            // transform direction of movement based on input
            movement = transform.TransformDirection(inputDir);
            
        }
    }
}
