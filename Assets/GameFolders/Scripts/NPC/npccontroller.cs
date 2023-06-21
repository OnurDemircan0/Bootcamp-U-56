using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npccontroller : MonoBehaviour
{
    GameObject Player;
    Animator animator;
    public int npchealth = 100;


    NPCAI movement;
    // Start is called before the first frame update
    void Start()

    {
        animator = GetComponent<Animator>();
        movement = GetComponent<NPCAI>();
        Player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(this.transform.position, Player.transform.position) <= 10)
        {
            animator.SetBool("npcattack", true);
            if (npchealth <= 0)
            {
                animator.SetBool("dead", true);
                movement._agent.SetDestination(this.transform.position);
            }
        }
        else
        {
            animator.SetBool("npcattack", false);
        }
    }
}
