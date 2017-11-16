using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    public class MoveWithinBounds : MonoBehaviour
    {
        public float movementSpeed = 20f;
        public float zoomSensitivity = 10f;
        public CameraBounds bounds;

        
        void Update()
        {
            Vector3 pos = transform.position;  // store position in smaller variable
            float inputH = Input.GetAxis("Horizontal");  // get input
            float inputV = Input.GetAxis("Vertical");

            Vector3 inputDir = new Vector3(inputH, 0f, inputV);  // store input in vector (for movement)
            pos += inputDir * movementSpeed * Time.deltaTime;

            float inputScroll = Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity;  // get scroll wheel
            Vector3 scrollDir = transform.forward * inputScroll;
            pos += scrollDir;

            pos = bounds.GetAdjustedPos(pos);  // overwrite original position with adjustedPos
            transform.position = pos;
        }
    }
}
