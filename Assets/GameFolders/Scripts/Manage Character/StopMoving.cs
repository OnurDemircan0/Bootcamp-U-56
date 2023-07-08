using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopMoving : MonoBehaviour

{
    [SerializeField]
    Animator medBOTanimator;
    public static StopMoving Instance { get; private set; }
    public bool DisableMove;
    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        StopAnim();
    }
    void StopAnim()
    {
        if(DisableMove)
        {
            medBOTanimator.SetFloat("Speed", 0f);
            medBOTanimator.SetBool("WalkBackAim", false);
            medBOTanimator.SetBool("ToMove", false);
            medBOTanimator.SetBool("FreeFall", false);
            medBOTanimator.SetBool("Jump", false);
        }
    }
}
