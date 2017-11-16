using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance1
{
    public class Charger : Enemy
    {
        [Header("Charger")]
        public float chargerSpeed = 20f;
        public float knockback = 5f;
        
    }
}
