using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float distance;

    GameObject Player;
    Animator animator;
    [SerializeField] int healthNPC = 100;
    private int _currentHealthNPC;
    private bool isDead;

    
    // Start is called before the first frame update
    NavMeshAgent _agent;
    void Start()

    {
        _agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        
        Player = GameObject.FindWithTag("Player");

        isDead = false;
        _currentHealthNPC = healthNPC;
    }

    private void OnEnable()
    {
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        distance =Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 5)
        {
            //animator.SetBool("npcattack", true);
            //attack
        }
        else if (distance<=50)
        {
            //animator.SetBool("npcattack", false);
            _agent.SetDestination(Player.transform.position);

        }
        else
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }


        if (_currentHealthNPC <= 0)
        {
            //animator.SetBool("dead", true);
            isDead = true;
        }

        

    }
}
