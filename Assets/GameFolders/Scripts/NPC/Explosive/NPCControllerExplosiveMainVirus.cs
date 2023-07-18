using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControllerExplosiveMainVirus : MonoBehaviour
{
    private float distance;

    GameObject Player;
    Animator animator;
    private bool attack;
    private float explosionDistance;


    // Start is called before the first frame update
    NavMeshAgent _agent;
    void Start()

    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        Player = GameObject.FindWithTag("Player");

        attack = false;
        explosionDistance = transform.GetChild(0).gameObject.transform.localScale.x / 20.0f;
    }

    // Update is called once per frame
    void Update()
    {
        distance =Vector3.Distance(transform.position, Player.transform.position);
        explosionDistance = transform.GetChild(0).gameObject.transform.localScale.x / 20.0f;

        if (!attack)
        {
            if (distance <= explosionDistance)
            {
                _agent.isStopped = true;
                _agent.ResetPath();
                animator.SetBool("Attack", true);
                attack = true;
                VirusCount.virusCount++;
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
        gameObject.SetActive(false);
    }

}
