using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Bullet : MonoBehaviour
    {
        public string triggerTag = "Asteroid";
        void OnTriggerEnter2D(Collider2D col)
        {
            //If col's tag is triggerTag
            if (col.tag == triggerTag)
            {
                //Destroy self
                Destroy(gameObject);
                //Destroy other
                Destroy(col.gameObject);
            }
        }
    }
}
