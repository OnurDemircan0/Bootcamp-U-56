using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static Enemy instance;


    public GameObject enemyGameObject;

    Animator animator;

    RagDollToggle ragDollToggle;

    EnemyAI enemyAI;

    


    [SerializeField] 
    ThirdPersonShooterController shooterController;

    public bool correctEnemy;
    public bool isShooting = false;

    string enemyTag;

    public float Health = 100;

    private void Start()
    {
        instance = this;

        ragDollToggle = GetComponent<RagDollToggle>();
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();

        enemyTag = gameObject.tag;
    }
    private void Update()
    {
        if (enemyGameObject != null && (enemyTag == "Enemy" || enemyTag == "Phase Two Enemy"))
        {
            CheckHealth();
            HitAnim();
            // SetShootAnim();
        }
        else
        {
            CheckHealthBoss();
        }
    }

    void CheckHealthBoss()
    {
        if(Health < 0)
        {
            Debug.Log("Enemy is dead");
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
            correctEnemy = false;
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

        if(enemyTag == "Enemy")
        {
            GetEnemies.Instance.CheckEnemyAliveState();
        }
        else
        {
            GetEnemies.Instance.CheckEnemyAliveStatePhaseTwo();
        }
        
        yield break;
    }
}
