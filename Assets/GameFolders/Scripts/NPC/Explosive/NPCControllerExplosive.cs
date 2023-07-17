using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControllerExplosive : MonoBehaviour
{
    private float distance;

    

    GameObject Player;
    Animator animator;
    private bool isDead;
    private bool attack;
    private float explosionDistance;

    
    // Start is called before the first frame update
    NavMeshAgent _agent;
    void Start()

    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        Player = GameObject.FindWithTag("Player");

        

        isDead = false;
        attack = false;
        explosionDistance = transform.GetChild(0).gameObject.transform.localScale.x / 20.0f;
    }

    private void OnEnable()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance =Vector3.Distance(transform.position, Player.transform.position);
        explosionDistance = transform.GetChild(0).gameObject.transform.localScale.x / 20.0f;

        if (!attack && !isDead)
        {
            if (distance <= explosionDistance)
            {
                _agent.isStopped = true;
                _agent.ResetPath();
                animator.SetBool("Attack", true);
                attack = true;
                Invoke(nameof(DisableObject), 2);
            }
            else
            {
                if (!attack)
                {
                    _agent.SetDestination(Player.transform.position);
                }
            }
        }

    }

    

    private void DisableObject()
    {
        Destroy(gameObject);
    }

}
