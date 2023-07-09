using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MimicSpace
{
    public class Movement : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Body Height from ground")]
        [Range(0.5f, 5f)]
        public float height = 0.8f;

        public float speed = 5f;
        public float velocityLerpCoef = 4f;

        public GameObject player;
        public float chaseDistance = 2f;
        public int damageAmount = 10;

        private NavMeshAgent agent;
        private Mimic myMimic;

        private bool alreadyAttacked = false;
        private bool isDeadCharacter = false;

        public float timeBetweenAttacks = 1f;
        public int mimicDamage = 20;

        private float attackCooldown = 0f;

        Death playerDeathScript;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            myMimic = GetComponent<Mimic>();
            playerDeathScript = player.GetComponent<Death>();
        }

        private void Update()
        {
            if (player != null)
            {
                agent.SetDestination(player.transform.position);

                float distance = Vector3.Distance(transform.position, player.transform.position);

                if (distance <= chaseDistance)
                {
                    if (!isDeadCharacter)
                    {
                        DealDamage(player);
                    }
                }
            }

            //if (attackCooldown > 0f)
            //{
            //    attackCooldown -= Time.deltaTime;
            //}
            //
            myMimic.velocity = agent.velocity;
            transform.position = agent.nextPosition + Vector3.up * height;
        }
        private void DealDamage(GameObject target)
        {
            if (!alreadyAttacked && !isDeadCharacter)
            {
                alreadyAttacked = true;

                playerDeathScript.TakeHit(mimicDamage);

                // Debug.Log("Attacking");
                // Reset the attack cooldown

               // CheckPlayerHealth();
                Invoke("ResetAttack", timeBetweenAttacks);
            }
        }

        private void CheckPlayerHealth()
        {
            if(playerDeathScript.health <= 0f)
            {
                isDeadCharacter = true;
            }
        }
        private void ResetAttack()
        {
            alreadyAttacked = false;
        }
    }
}
