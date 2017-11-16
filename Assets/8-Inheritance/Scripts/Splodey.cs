using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance
{
    public class Splodey : Enemy
    {
        [Header("Splodey")]
        public float explosionRadius = 10f;
        public float explosionRate = 1f;
        public float impactForce = 10f;
        public GameObject explosionPaticles;

        private float explosionTimer = 0f;

        protected override void Attack()
        {
            // if explosionTimer reaches rate
            if (explosionTimer >= explosionRate)
            {
                // call Explode
                Explode();
            }
        }

        protected override void OnAttackEnd()
        {
            explosionTimer = 0f;
        }

        public void Explode()
        {
            // perform overlap sphere
            Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);
            // Foreach hit in hits
            foreach (Collider hit in hits)
            {
                Health h = hit.GetComponent<Health>();
                // if has health
                if (h != null)
                {
                    // decrease health 
                    h.TakeDamage(damage);
                    // add force to player's rigid
                    rigid.AddExplosionForce(impactForce, transform.position, explosionRadius);
                }
            }
        }
                
        protected override void Update()
        {
            base.Update();
            // start Explosion Timer
            explosionTimer += Time.deltaTime;
        }
    }
}