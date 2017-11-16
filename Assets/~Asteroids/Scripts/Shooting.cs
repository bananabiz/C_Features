using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class Shooting : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float bulletSpeed = 20f;
        public float shootRate = 0.2f;

        private float shootTimer = 0f;

       void Shoot()
        {
            // Create a new bullet
            GameObject clone = Instantiate(bulletPrefab, transform.position, transform.rotation);
            // Grab rigidbody2D form bullet clone
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            // Add force using bullet speed
            rigid.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
        }

        // Update is called once per frame
        void Update()
        {
            // count up timer
            shootTimer += Time.deltaTime;
            if (shootTimer > shootRate)
            {
                if (Input.GetKey(KeyCode.Space))  // if space is pressed
                {
                    Shoot();   // shoot bullet
                    shootTimer = 0f;   // reset timer
                }
            }
        }
    }
}
