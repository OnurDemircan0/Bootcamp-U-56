using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slidev3 : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    LayerMask slideLayer;

    [SerializeField]
    Transform slideChecker;

    [SerializeField]
    float checkRadius = 0.75f;

    

    bool onSlideLayer;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(slideChecker.transform.position, checkRadius);
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        onSlideLayer = Physics.CheckSphere(slideChecker.transform.position, checkRadius, slideLayer);

        if (onSlideLayer )
        { 
            animator.SetLayerWeight(4, 1f);
        }

        else
        {
            animator.SetLayerWeight(4, 0f);
        }
    }

}
