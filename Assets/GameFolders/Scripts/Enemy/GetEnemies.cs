using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class GetEnemies : MonoBehaviour
{
    public GameObject[] ThingsToMove;

    public GameObject[] SpawnPoints;

    public static GetEnemies Instance;

    GameObject[] enemies;

    GameObject[] enemiesPhase2;

    public GameObject phase2Parent;

    bool[] enemyAliveState;
    bool[] enemyAliveStatePhaseTwo;

    [SerializeField]
    float clearBloodDuration = 5f;

    [SerializeField]
    float riseBloodDuration = 15f;

    float timer = 0.0f;

    int j = 0;
    int a;
    int enemySpawned = 0;



    private void Start()
    {
        Instance = this;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("Enemy number is " + enemies.Length);

        enemyAliveState = new bool[enemies.Length];

        enemiesPhase2 = phase2Parent.GetComponentsInChildren<Transform>()
            .Where(t => t != phase2Parent.transform)
            .Select(t => t.gameObject)
            .ToArray();
        foreach (GameObject enemy in enemiesPhase2) { enemy.SetActive(false); }

        CheckEnemyAliveState();
    }


    public void CheckEnemyAliveState()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemyAliveState[i] = enemies[i].activeSelf;

            Debug.Log("Enemy " + i + " alive state: " + enemyAliveState[i]);
        }

        bool allFalse = enemyAliveState.All(value => value == false);
        if (allFalse)
        {
            // Call Function if all the enemies are death
            Debug.Log("All Enemies are Death");

            
            if(enemySpawned < enemies.Length)
            {
                ClearBlood();
                ActivateEnemies();
            }
            else if (enemySpawned >= enemies.Length)
            {
                Debug.Log("Phase 2 Starting");
                RiseBlood();
                StartPhaseTwo();
            }

        }
    }

    public void CheckEnemyAliveStatePhaseTwo()
    {
        for (int i = 0; i < enemiesPhase2.Length; i++)
        {
            enemyAliveStatePhaseTwo[i] = enemiesPhase2[i].activeSelf;

           // Debug.Log("Enemy " + i + " alive state: " + enemyAliveState[i]);
        }

        bool allFalse = enemyAliveStatePhaseTwo.All(value => value == false);
        if (allFalse)
        {
            // Call Function if all the enemies are death
            //Debug.Log("All Enemies are Death");

            Debug.Log("Do Something Here");

        }
    }



    private IEnumerator ClearBloodCoroutine()
    {
        yield return new WaitForSeconds(4);

        Vector3 originalPosF = ThingsToMove[0].transform.position;
        float targetY = originalPosF.y - 56.5f;

        Vector3 originalPosS = ThingsToMove[1].transform.position;
        float targetYS = originalPosS.y - 56.5f;

        Vector3 originalPosT = ThingsToMove[2].transform.position;
        float targetYT = originalPosT.y - 56.5f;

        float elapsedTime = 0f;

        while (elapsedTime < clearBloodDuration)
        {
            float t = elapsedTime / clearBloodDuration;

            float newY = Mathf.Lerp(originalPosF.y, targetY, t);
            float newYS = Mathf.Lerp(originalPosS.y, targetYS, t);
            float newYT = Mathf.Lerp(originalPosT.y, targetYT, t);

            // Set the new position of the objects
            ThingsToMove[0].transform.position = new Vector3(originalPosF.x, newY, originalPosF.z);
            ThingsToMove[1].transform.position = new Vector3(originalPosS.x, newYS, originalPosS.z);
            ThingsToMove[2].transform.position = new Vector3(originalPosT.x, newYT, originalPosT.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    private IEnumerator RiseBloodCoroutine()
    {
        Debug.Log("Rising Blood");

        Vector3 originalPosF = ThingsToMove[0].transform.position;
        float targetY = originalPosF.y + 11f;

        Vector3 originalPosS = ThingsToMove[1].transform.position;
        float targetYS = originalPosS.y + 11f;

        Vector3 originalPosT = ThingsToMove[2].transform.position;
        float targetYT = originalPosT.y + 11f;

        float elapsedTime = 0f;

        while (elapsedTime < riseBloodDuration)
        {
            float t = elapsedTime / riseBloodDuration;

            float newY = Mathf.Lerp(originalPosF.y, targetY, t);
            float newYS = Mathf.Lerp(originalPosS.y, targetYS, t);
            float newYT = Mathf.Lerp(originalPosT.y, targetYT, t);

            // Set the new position of the objects
            ThingsToMove[0].transform.position = new Vector3(originalPosF.x, newY, originalPosF.z);
            ThingsToMove[1].transform.position = new Vector3(originalPosS.x, newYS, originalPosS.z);
            ThingsToMove[2].transform.position = new Vector3(originalPosT.x, newYT, originalPosT.z);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }

    void ClearBlood()
    {
        StartCoroutine(ClearBloodCoroutine());
    }

    void RiseBlood()
    {
        StartCoroutine(RiseBloodCoroutine());
    }

    void StartPhaseTwo()
    {
        Debug.Log("Trying to Activate Phase Two Enemies");
        foreach (GameObject enemy in enemiesPhase2)
        {
            enemy.SetActive(true);
            Debug.Log("Enemy set true");
        }

    }
    void ActivateEnemies()
    {
        Debug.Log("Insýde The Activate Enemies");

        int enemyCount = enemies.Length;
        int spawnPointCount = SpawnPoints.Length;

        int enemiesPerSpawnPoint = enemyCount / spawnPointCount;
        int remainingEnemies = enemyCount % spawnPointCount; // Enemies that cannot be evenly distributed



        int enemyIndex = 0;

        for (int i = 0; i < spawnPointCount; i++)
        {
            Debug.Log("Calculating Spawn Point" + i);
            Vector3 spawnPos = SpawnPoints[i].transform.position;

            // Spawn the enemies per spawn point
            for (int c = 0; c < enemiesPerSpawnPoint; c++)
            {
               // float RandomX = Random.Range(1f, 55f);
               // float RandomZ = Random.Range(1f, 55f);
               // spawnPos.x += RandomX;
               // spawnPos.z += RandomZ;

                Debug.Log("Distrubuting Enemies");
                SpawnEnemy(spawnPos, enemyIndex);
                enemyIndex++;
            }

            // Handle the remaining enemies
            if (remainingEnemies > 0)
            {
                Debug.Log("Spawning Remaning Enemies");
                SpawnEnemy(spawnPos, enemyIndex);
                enemyIndex++;
                remainingEnemies--;
            }
        }
    }

    void SpawnEnemy(Vector3 spawnPosition, int enemyIndex)
    {
        Debug.Log("Spawning Enemies");
        if (enemyIndex >= 0 && enemyIndex < enemies.Length)
        {
            Debug.Log("Spawning Enemy" + enemies[enemyIndex].name);

            GameObject enemy = enemies[enemyIndex];

           MoveObjectWithOther enemyObjectAttach = enemy.GetComponent<MoveObjectWithOther>();

            if(enemyObjectAttach != null)
            {
                enemyObjectAttach.enabled = false;
            }

            enemy.GetComponent<NavMeshAgent>().enabled = true;
            enemy.GetComponent<Enemy>().Health = 110;
            enemy.GetComponent<RagDollToggle>().RagdollEnable(false);
            enemy.GetComponent<ProjectileAttack>().enabled = false;
            enemy.GetComponent<EnemyAI>().enabled = true;
           

            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            

            enemy.gameObject.SetActive(true);

            enemySpawned++;
        }
    }
}
