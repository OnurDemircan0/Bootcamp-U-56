using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class npccontroller : MonoBehaviour
{
    public float distance;

    GameObject Player;
    Animator animator;
    public int npchealth = 100;


    
    // Start is called before the first frame update
    NavMeshAgent _agent;
    void Start()

    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        Player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        distance =Vector3.Distance(this.transform.position, Player.transform.position);
        if (distance <= 5)
        {
            animator.SetBool("npcattack", true);
            //attack
        }
        else if (distance<=50)
        {
            animator.SetBool("npcattack", false);
            _agent.SetDestination(Player.transform.position);

        }
        else
        {
            _agent.SetDestination(this.transform.position);

        }


        if (npchealth <= 0)
        {
            animator.SetBool("dead", true);

        }
    }
}
