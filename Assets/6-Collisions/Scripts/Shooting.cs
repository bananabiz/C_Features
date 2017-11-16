using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Collisions
{
    [RequireComponent(typeof(AudioSource))]
    public class Shooting : MonoBehaviour
    {
        public GameObject muzzleFlashPrefab;
        public GameObject projectilePrefab;
        public Transform spawnPoint;
        public float projectileSpeed = 20;
        public float shootRate = 0.15f;
        [Header("Fire Sound")]
        [Range(-3, 3)]
        public float minPitch = 1;
        [Range(-3, 3)]
        public float maxPitch = 1;

        private float shootTimer = 0f;

        private AudioSource sound;

        void Start()
        {
            sound = GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {
            // Increase shootTimer with deltaTime
            shootTimer += Time.deltaTime;
            // Check IF space bar is pressed AND IF shootTimer >= shootRate
            if (Input.GetKey(KeyCode.Space) && shootTimer >= shootRate)
            {
                // CALL Shoot()
                Shoot();
                // SET shootTimer = 0
                shootTimer = 0;
            }
        }

        void Shoot()
        {
            // Instantiate a new projectile
            GameObject clone = Instantiate(projectilePrefab);
            // Set position to player
            clone.transform.position = spawnPoint.position;
            // Grab Rigidbody from clone
            Rigidbody2D rigid = clone.GetComponent<Rigidbody2D>();
            // Send it flying!
            rigid.AddForce(transform.up * projectileSpeed, ForceMode2D.Impulse);
            // Instantiate muzzleFlashPrefab
            Instantiate(muzzleFlashPrefab, spawnPoint.position, Quaternion.identity);
            // Randomize pitch
            sound.pitch = Random.Range(minPitch, maxPitch);
            // Play "jackhammer" sound
            sound.Play();
        }
    }
}