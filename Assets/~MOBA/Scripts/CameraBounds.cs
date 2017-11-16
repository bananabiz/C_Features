using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MOBA
{
    public class CameraBounds : MonoBehaviour
    {
        public Vector3 size = new Vector3(50f, 15f, 80f);

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(transform.position, size);
        }

        // takes in current pos and returns adjusted Pos (constrained)
        public Vector3 GetAdjustedPos(Vector3 incomingPos)
        {
            // store position in smaller variable
            Vector3 pos = transform.position;
            Vector3 halfSize = size * 0.5f;

            // Is incompingPos.z outside positive z?
            if (incomingPos.z > pos.z + halfSize.z)
            {
                incomingPos.z = pos.z + halfSize.z;
            }
            // Is incompingPos.z outside negative z?
            if (incomingPos.z < pos.z - halfSize.z)
            {
                incomingPos.z = pos.z - halfSize.z;
            }
            // Is incompingPos.y outside positive y?
            if (incomingPos.y > pos.y + halfSize.y)
            {
                incomingPos.y = pos.y + halfSize.y;
            }
            // Is incompingPos.y outside negative y?
            if (incomingPos.y < pos.y - halfSize.y)
            {
                incomingPos.y = pos.y - halfSize.y;
            }
            // Is incompingPos.x outside positive x?
            if (incomingPos.x > pos.x + halfSize.x)
            {
                incomingPos.x = pos.x + halfSize.x;
            }
            // Is incompingPos.x outside negative x?
            if (incomingPos.x < pos.x - halfSize.x)
            {
                incomingPos.x = pos.x - halfSize.x;
            }
            return incomingPos;
        }
        
    }
}
