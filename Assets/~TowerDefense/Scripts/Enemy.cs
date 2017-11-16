using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Enemy : MonoBehaviour
    {
        public float health = 100f; //enemie's health which starts at 100

        //  <access-specifier>  <return-type>   <function-name> (<argument>, <argument>)
        public void DealDamage(float damage)
        {
            //set health -= damage
            health -= damage;
            // if health <=0
            if (health <= 0)
            {
                //destroy the enemy
                Destroy(this.gameObject);
            }
        }

    }
}
