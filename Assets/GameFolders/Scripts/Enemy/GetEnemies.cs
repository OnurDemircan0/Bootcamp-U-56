using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetEnemies : MonoBehaviour
{
    public GameObject[] ThingsToMove;

    public GameObject[] SpawnPoints;

    public static GetEnemies Instance;

    GameObject[] enemies;

    bool[] enemyAliveState;

    [SerializeField]
    float clearBloodDuration = 5f;

    float timer = 0.0f;

    int j = 0;
    int a;



    private void Start()
    {
        Instance = this;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        Debug.Log("Enemy number is " + enemies.Length);

        enemyAliveState = new bool[enemies.Length];

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

            ClearBlood();
        }
    }



    private IEnumerator ClearBloodCoroutine()
    {
        yield return new WaitForSeconds(4);

        Vector3 originalPosF = ThingsToMove[0].transform.position;
        float targetY = originalPosF.y - 120f;

        Vector3 originalPosS = ThingsToMove[1].transform.position;
        float targetYS = originalPosS.y - 120f;

        Vector3 originalPosT = ThingsToMove[2].transform.position;
        float targetYT = originalPosT.y - 120f;

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

        ActivateEnemies();
    }

    void ClearBlood()
    {
        StartCoroutine(ClearBloodCoroutine());
    }


    void ActivateEnemies()
    {
        int enemyCount = enemies.Length;
        int spawnPointCount = SpawnPoints.Length;

        int enemiesPerSpawnPoint = enemyCount / spawnPointCount;
        int remainingEnemies = enemyCount % spawnPointCount; // Enemies that cannot be evenly distributed

        int enemyIndex = 0;

        for (int i = 0; i < spawnPointCount; i++)
        {
            Vector3 spawnPos = SpawnPoints[i].transform.position;

            // Spawn the enemies per spawn point
            for (int j = 0; j < enemiesPerSpawnPoint; j++)
            {
                SpawnEnemy(spawnPos, enemyIndex);
                enemyIndex++;
            }

            // Handle the remaining enemies
            if (remainingEnemies > 0)
            {
                SpawnEnemy(spawnPos, enemyIndex);
                enemyIndex++;
                remainingEnemies--;
            }
        }
    }

    void SpawnEnemy(Vector3 spawnPosition, int enemyIndex)
    {
        if (enemyIndex >= 0 && enemyIndex < enemies.Length)
        {
            GameObject enemy = enemies[enemyIndex];
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
