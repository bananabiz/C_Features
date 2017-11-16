using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collisions
{
    public class MuzzleFlash : MonoBehaviour
    {
        public int maxFrames = 1;
        private int currentFrames = 0;
        // Update is called once per frame
        void Update()
        {
            // If the frame gets to max frames or greater
            if(currentFrames >= maxFrames)
            {
                Destroy(gameObject);
            }
            // Count each frame
            currentFrames++;
        }
    }
}