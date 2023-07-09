using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Transform frontLeg;

    public float frontLegRadius = 0.5f;

    public int legAttackDamage = 15;
    
    Animator animator;

    [SerializeField]
    Death death;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;
    private bool isDeathCharacter = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("MedBOT").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;

        animator.SetBool("Walking", true);

        //animator.SetBool("Arm Attack", false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 100f));
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        animator.SetBool("Walking", true);

        agent.SetDestination(player.position);

        //animator.SetBool("Arm Attack", false);
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 100f));
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked && !isDeathCharacter)
        {
            ///Animation Sets
            animator.SetBool("Walking", false);
            //animator.SetBool("Arm Attack", true);
            animator.SetLayerWeight(1, 1);

            ///Attack code here
            bool legAttack = Physics.CheckSphere(frontLeg.transform.position, frontLegRadius, whatIsPlayer);
            if (legAttack) 
            {   TakeDamage(legAttackDamage);

                ///End of attack code
                alreadyAttacked = true;
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
            
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        death.TakeHit(damage);

        if(death.health < 0)
        {
            StopAttacking();
        }
    }
    public void StopAttacking()
    {
        if(!isDeathCharacter)
        {
            animator.SetLayerWeight(1,0);
            isDeathCharacter = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(frontLeg.transform.position, frontLegRadius);
    }
}