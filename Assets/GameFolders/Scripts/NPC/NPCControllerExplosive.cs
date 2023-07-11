using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCControllerExplosive : MonoBehaviour
{
    private float distance;

    GameObject Player;
    Animator animator;
    [SerializeField] int healthNPC = 100;
    private int _currentHealthNPC;
    private bool isDead;
    private bool attack;

    
    // Start is called before the first frame update
    NavMeshAgent _agent;
    void Start()

    {
        _agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        
        Player = GameObject.FindWithTag("Player");

        isDead = false;
        attack = false;
        _currentHealthNPC = healthNPC;
    }

    private void OnEnable()
    {
        isDead = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("VFXotherbullet"))
        {
            Debug.Log("musthappen");
            _agent.isStopped = true;
            _agent.ResetPath();
            animator.SetBool("Attack", true);
            attack = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance =Vector3.Distance(transform.position, Player.transform.position);
        if (distance <= 5)
        {
            _agent.isStopped = true;
            _agent.ResetPath();
            animator.SetBool("Attack", true);
            attack = true;
        }
        else if (distance<=50)
        {
            if (!attack)
            {
                _agent.SetDestination(Player.transform.position);
            }
        }
        else
        {
            _agent.isStopped = true;
            _agent.ResetPath();
        }


        if (_currentHealthNPC <= 0)
        {
            isDead = true;
        }

        

    }
}
