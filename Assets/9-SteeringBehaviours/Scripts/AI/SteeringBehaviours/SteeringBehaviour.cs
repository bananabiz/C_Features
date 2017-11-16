using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [RequireComponent(typeof(AIAgent))]
    public class SteeringBehaviour : MonoBehaviour
    {
        public float weight = 8f;
        [HideInInspector]
        public AIAgent owner;

        public virtual void Awake()
        {
            owner = GetComponent<AIAgent>();
        }

        public virtual Vector3 GetForce()
        {
            return Vector3.zero;
        }
    }
}
