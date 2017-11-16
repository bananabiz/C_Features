using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PaperMarioClone
{
    [RequireComponent(typeof(PlayerController))]
    public class UserInput : MonoBehaviour
    {
        private PlayerController pController;
        
        void Start()
        {
            pController = GetComponent<PlayerController>(); 
        }

        
        void Update()
        {
            // get inputH and inputV
            float inputH = Input.GetAxis("Horizontal");
            float inputV = Input.GetAxis("Vertical");
            // move the PlayerController
            pController.Move(inputH, inputV);
            // is space being pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pController.Jump();  //jump
            }
        }
    }
}
