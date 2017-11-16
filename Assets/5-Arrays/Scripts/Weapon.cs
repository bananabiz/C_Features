using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arrays
{
    public class Weapon : MonoBehaviour
    {
        public int damage = 10;
        public int maxBullets = 100;
        public float fireInterval = 0.2f;
        public GameObject bulletPrefab;
        public Transform spawnPoint;

        private Transform target;
        private bool isFired = false;
        private int currentBullets = 0;
        private Bullet[] spawnedBullets; //null by default

        // Use this for initialization
        void Start()
        {
            spawnedBullets = new Bullet[maxBullets];
        }

        // Update is called once per frame
        void Update()
        {
            // If !isFired && currentBullets < maxBullets
            if (!isFired && currentBullets < maxBullets)
            {
                // Fire!
                StartCoroutine(Fire());
            }

            /*
            fireTimer += Time.deltaTime;
            if(fireTimer >= fireInterval)
            {
            SpawnBullet();  //Same as below script
            }
            */
        }

        IEnumerator Fire()
        {
            // Run whatever is at the top of this function
            isFired = true;
            // Spawn the bullet
            SpawnBullet();

            // Run whatever is at the top of this function
            yield return new WaitForSeconds(fireInterval); //Wait for fire interval to finish
                                                           // Run whatever is here after fireInterval
            isFired = false;
        }

        void SpawnBullet()
        {
            //1. Instantiate a bullet clone
            GameObject clone = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

            //2. Create direction variable for bullet and rotation
            Vector2 direction = target.position - transform.position;
            direction.Normalize();

            //3. Rotate the weapon to direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //4. Grab the bullet script from clone
            Bullet bullet = clone.GetComponent<Bullet>();

            //5. Send bullet to target (by setting direction)
            bullet.direction = direction;

            //6. Store bullet in array
            spawnedBullets[currentBullets] = bullet;

            //7. Increment currentBullets
            currentBullets++;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}