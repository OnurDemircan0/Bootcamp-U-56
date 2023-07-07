using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollToggle : MonoBehaviour
{
    Animator animator;
    BoxCollider boxCollider;

    CapsuleCollider[] childCapsuleColliders;
    Rigidbody[] childRigidbodies;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        childCapsuleColliders = GetComponentsInChildren<CapsuleCollider>();
        childRigidbodies = GetComponentsInChildren<Rigidbody>();

        RagdollEnable(false);
    }


    public void RagdollEnable(bool active)
    {
        if (childCapsuleColliders !=null)
        {
            foreach (CapsuleCollider c in childCapsuleColliders) { c.enabled = active; }
        }

        if (childRigidbodies != null)
        {
            foreach (Rigidbody c in childRigidbodies) { c.detectCollisions = active; c.isKinematic = !active; }
        }

        animator.enabled = !active;
        boxCollider.enabled = !active;
    }

}
