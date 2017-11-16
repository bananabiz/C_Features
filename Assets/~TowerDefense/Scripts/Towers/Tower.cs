using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        public Cannon cannon; //Reference to cannon inside of tower
        public float attackRate = 0.25f; //Rate of attack in seconds
        public float attackRadius = 5f; //Distance of attack in world units
        private float attackTimer = 0f; //Timer to count up to attackRate
        private List<Enemy> enemies = new List<Enemy>(); //List of enemies within radius

        void OnTriggerEnter(Collider col)
        {
            //Let e = col's Enemy component
            Enemy e = col.GetComponent<Enemy>();
            //If e != null
            if (e != null)
            {
                //Add e to enemies list
                enemies.Add(e);
            }
        }

        void OnTriggerExit(Collider col)
        {
            //Let e = col's Enemy component
            Enemy e = col.GetComponent<Enemy>();
            //If e != null
            if (e != null)
            {
                //Remove e from enemies list
                enemies.Remove(e);
            } 
        }

        
        List<Enemy> RemoveAllNulls(List<Enemy> listWithNulls)
        {
            //Let listWithoutNulls = new List
            List<Enemy> listWithoutNulls = new List<Enemy>();


            // >> Algorithm goes here <<
            foreach (Enemy e in listWithNulls)
            {
                if (e != null)
                {
                    listWithoutNulls.Add(e);
                }
            }
                //Return listWithoutNulls
                return listWithoutNulls;
        }


        Enemy GetClosestEnemy()
        {
            //Set enemies = RemoveAllNulls(enemies)
            enemies = RemoveAllNulls(enemies);
            //Let closest = null
            Enemy closest = null;
            //Let minDistance = float.MaxValue
            float minDistance = float.MaxValue;
            //Foreach enemy in enemies
            foreach (Enemy enemy in enemies)
            {
                //Let distance =  the distance between transform's position and enemy's position
                float distance = (transform.position - enemy.transform.position).magnitude;
                //If distance < minDistance
                if (distance < minDistance)
                {
                    //Set minDistance = distance
                    minDistance = distance;
                    //Set closest = enemy
                    closest = enemy;
                }
            }
            //Return closest, return null if none were found
            return closest;
        }

        
        int AddTwoNumbers(int a, int b)
        {
            return a + b;
        }

        void Attack()
        {
            //Let closest to GetClosestEnemy()
            Enemy closest = GetClosestEnemy();
            //If closest != null
            if (closest != null)
            {
                //Call cannon.Fire() and pass closest as argument
                cannon.Fire(closest);
            }
        }

        // Use this for initialization
        void Start()
        {      

        }

        // Update is called once per frame
        void Update()
        {
            //Set attackTimer = attackTimer + deltaTime
            attackTimer += Time.deltaTime;
            //If attackTimer >= attackRate
            if (attackTimer >= attackRate)
            {
                //Call Attack()
                Attack();
                //Set attackTimer = 0
                attackTimer = 0;
            }
        }
    }
}
