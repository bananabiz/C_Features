using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Cannon : MonoBehaviour
    {
        public Transform barrel; //Reference to barrel where bullet will be shot from
        public GameObject projectilePrefab; //Prefab of projectile to instantiate when firing

        public void Fire(Enemy targetEnemy)
        {
            //Let targetPos = targetEnemy's position
            Vector3 targetPos = targetEnemy.transform.position;
            //Let barrelPos = barrel's position
            Vector3 barrelPos = barrel.transform.position;
            //Let barrelRot = barrel's rotation
            Quaternion barrelRot = barrel.transform.rotation;
            //Let fireDirection = targetPos - barrelPos
            Vector3 fireDirection = targetPos - barrelPos;
            //Set cannon's rotation = Quaternion.LookRotation(fireDirection, Vector3.up)
            transform.rotation = Quaternion.LookRotation(fireDirection); //Vector3.up by default, dosen't need to write it if upward 
            //Let clone = Instantiate(projectilePrefab, barrelPos, barrelRot)
            GameObject clone = Instantiate(projectilePrefab, barrelPos, barrelRot);
            //Let p = clone's Projectile component
            Projectile p = clone.GetComponent<Projectile>();
            //Set p.direction = fireDirection
            p.direction = fireDirection;
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
