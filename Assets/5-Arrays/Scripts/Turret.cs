using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// LitJSON
// JSON = JavaScript Object Notation

namespace Arrays
{
    public class Turret : MonoBehaviour
    {

        public Transform target;

        private Weapon currentWeapon;

        // Use this for initialization
        void Start()
        {

            currentWeapon = GetComponentInChildren<Weapon>();
            currentWeapon.SetTarget(target);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
