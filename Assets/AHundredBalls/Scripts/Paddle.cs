using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AHundredBalls
{
    public class Paddle : MonoBehaviour
    {
        public bool isOpened = false;

        private Animator anim;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isOpened = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isOpened = !isOpened;
            }
            UpdateAnimation(); 
        }

        void UpdateAnimation()
        {
            anim.SetBool("isOpened", isOpened);
        }
    }
}
