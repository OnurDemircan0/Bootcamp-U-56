using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    [SerializeField] 
    GameObject bullet;

    public
    Transform bulletSpawnPoint;

    public
    Transform medBOT;

    public
    float projectileDistance;

    [SerializeField]
    float shootDelay = 0.1f;

    [SerializeField]
    Death MedBotDeath;

    ObjectPooler pooler;

    private float lastShootTime;

    Enemy enemyScript;

    EnemyAI enemyAI;

    Transform rayHitTransform;
    private void Start()
    {
        pooler = ObjectPooler.instance;
        enemyScript = GetComponent<Enemy>();

        enemyAI = GetComponent<EnemyAI>();
    }
    private void Update()
    {
      // Vector3 shootDirection = (medBOT.position - bulletSpawnPoint.position).normalized;
      // Ray ray = new Ray(bulletSpawnPoint.position, shootDirection * projectileDistance);
      // Debug.DrawRay(bulletSpawnPoint.position, shootDirection * projectileDistance);
      // if (Physics.Raycast(ray, out RaycastHit raycastHit, projectileDistance))
      // {
      //     rayHitTransform = raycastHit.transform;
      // }

        float elapsedTime = Time.time - lastShootTime;
        if(enemyAI.enabled == false)
        {
            if (enemyScript.Health > 0 && elapsedTime >= shootDelay && MedBotDeath.health >= 0)
            {
                ProjectileStart();
            }
        }
        else
        {
            if (enemyScript.Health > 0 && elapsedTime >= shootDelay && MedBotDeath.health >= 0)
            {
                ProjectileWithEnemyAI();
            }
        }

        
        
    }
    void ProjectileStart()
    {
        float distance = Vector3.Distance(bulletSpawnPoint.position, medBOT.position);

        if (distance < projectileDistance) 
        {
            Vector3 shootDirection = (medBOT.position - bulletSpawnPoint.position).normalized;
            pooler.SpawnFromPool("EnemyBullet", bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));

            lastShootTime = Time.time;

            //enemyScript.isShooting = true;
        }
        else { /* enemyScript.isShooting  = false; */ return; }

        
    }

    void ProjectileWithEnemyAI()
    {
        float distance = Vector3.Distance(bulletSpawnPoint.position, medBOT.position);

        if (distance < projectileDistance && !enemyAI.playerInSightRange)
        {
            Vector3 shootDirection = (medBOT.position - bulletSpawnPoint.position).normalized;
            pooler.SpawnFromPool("EnemyBullet", bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));

            lastShootTime = Time.time;

            //enemyScript.isShooting = true;
        }
        else { /* enemyScript.isShooting  = false; */ return; }
    }
}
