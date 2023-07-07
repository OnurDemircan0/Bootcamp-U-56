using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;
    public float Health = 100;
    public GameObject enemyGameObject;
    RagDollToggle ragDollToggle;
    EnemyAI enemyAI;

    [SerializeField] ThirdPersonShooterController shooterController;

    public bool correctEnemy;
    public bool isShooting = false;


    private void Start()
    {
        ragDollToggle = GetComponent<RagDollToggle>();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();
    }
    private void Update()
    {
        if (enemyGameObject != null)
        {
            CheckHealth(); 
            HitAnim();
           // SetShootAnim();
        }
    }

    void CheckHealth()
    {
        if (Health < 0)
        {
            ragDollToggle.RagdollEnable(true);
            enemyAI.enabled = false;
            
            StartCoroutine(WaitBeforeDisable());
        }
    }

    void HitAnim()
    {
        if(shooterController.EnemyHit && correctEnemy)
        {
            animator.SetBool("Hit", true);
        }
        else
        {
            animator.SetBool("Hit", false);
        }
    }

   // void SetShootAnim()
   // {
   //     if(isShooting)
   //     {
   //         animator.SetBool("Shooting", true);
   //     }
   //     else
   //     {
   //         animator.SetBool("Shooting", false);
   //     }
   // }

    IEnumerator WaitBeforeDisable()
    {
        yield return new WaitForSeconds(10f);

        enemyGameObject.SetActive(false);
        GetEnemies.Instance.CheckEnemyAliveState();
        yield break;
    }
}
