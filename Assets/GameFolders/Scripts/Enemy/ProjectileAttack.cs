using System.Collections;
using System.Collections.Generic;
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

    Transform rayHitTransform;
    private void Start()
    {
        pooler = ObjectPooler.instance;
        enemyScript = GetComponent<Enemy>();
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

        if (enemyScript.Health > 0 && elapsedTime >= shootDelay && MedBotDeath.health >= 0)
        {
            ProjectileStart();
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
}
