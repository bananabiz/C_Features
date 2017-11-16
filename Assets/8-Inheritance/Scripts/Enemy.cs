using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Inheritance
{
    [RequireComponent(typeof(Rigidbody))]
    public class Enemy : MonoBehaviour
    {
        [Header("Enemy")]
        public int health = 100;
        public int damage = 20;
        public float attackDuration = 2f;
        public float attackRate = 5f;
        public float attackRadius = 10f;
        public Transform target;

        private float attackTimer = 0f;
        protected NavMeshAgent nav;
        protected Rigidbody rigid;

        void Awake()
        {
            nav = GetComponent<NavMeshAgent>();
            rigid = GetComponent<Rigidbody>(); 
        }

        // Virtual function
        protected virtual void Attack(){}
        protected virtual void OnAttackEnd(){}

        IEnumerator AttackDelay(float delay)
        {
            // stop nav
            nav.Stop();
            yield return new WaitForSeconds(delay);
            // resume nav 
            nav.Resume();
            // call OnAttackEnd
            OnAttackEnd();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (target == null)
                return;
            // Set navigation to follow target
            nav.SetDestination(target.position);
            attackTimer += Time.deltaTime;

            // if timeer reaches attack rate
            if (attackTimer >= attackRate)
            {
                float distance = Vector3.Distance(transform.position, target.position);
                // if distance is within attack range
                if (distance <= attackRadius)
                {
                    // call Attack()
                    Attack();
                    // reset attackTimer
                    attackTimer = 0f;
                    // StartCoroutine 
                    StartCoroutine(AttackDelay(attackDuration));
                }
            }
        }
    }
}
