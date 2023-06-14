using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npccontroller : MonoBehaviour
{
    Animator animator;
    public int npchealth = 100;


    NPCAI movement;
    // Start is called before the first frame update
    void Start()

    {
        animator = GetComponent<Animator>();
        movement = GetComponent<NPCAI>();

    }

    // Update is called once per frame
    void Update()
    {
        if (npchealth<=0)
        {
            animator.SetBool("dead", true);
            movement._agent.SetDestination(this.transform.position);
        }
    }
}
