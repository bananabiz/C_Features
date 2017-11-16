using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {
    public GameObject projectilePrefab;
    public float projectileSpeed = 30f;
    public float acceleration = 36f;
    public float shootRate = 0.1f;
    public float shootTimer = 0f;

    // Update is called once per frame
    void Update() {
        shootTimer += Time.deltaTime;
        if (Input.GetKey(KeyCode.Space) && shootTimer >= shootRate)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void Shoot()
    { 
        GameObject projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;

        Rigidbody2D projectileRigid = projectile.GetComponent<Rigidbody2D>();
        projectileRigid.AddForce(transform.right * projectileSpeed * acceleration); 
	}
}
