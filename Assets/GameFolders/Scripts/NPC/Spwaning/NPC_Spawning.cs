using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Spawning : MonoBehaviour
{
    [SerializeField] GameObject[] NPC;
    [SerializeField] float range = 80.0f;
    [SerializeField] float spawnDelay = 1f;
    [SerializeField] int border1 = 4; //border1 must be smaller than border2.
    [SerializeField] int border2 = 10;
    private bool canSpawn = true;

    private void Start()
    {
        if(CheckPointSystem.checkPointNumber == 4)
        {
            this.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            if(CheckPointSystem.checkPointNumber == 0)
            {
                spawnDelay = 7;
                border1 = 20;
                border2 = 20;
            }
            else if(CheckPointSystem.checkPointNumber == 1)
            {
                spawnDelay = 6;
                border1 = 15;
                border2 = 20;
            }
            else if (CheckPointSystem.checkPointNumber == 2)
            {
                spawnDelay = 5;
                border1 = 12;
                border2 = 20;
            }
            else if (CheckPointSystem.checkPointNumber == 3)
            {
                spawnDelay = 4;
                border1 = 12;
                border2 = 18;
            }
            else if (CheckPointSystem.checkPointNumber == 4)
            {
                spawnDelay = 4;
                border1 = 7;
                border2 = 15;
            }
        }
    }

    private void Update()
    {
        if (canSpawn)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        float randomX = Random.Range(0, range);
        float axisX = randomX * (Random.Range(0, 2) * 2 - 1);
        float axisZ = Mathf.Sqrt(Mathf.Pow(range, 2) - Mathf.Pow(randomX, 2)) * (Random.Range(0, 2) * 2 - 1);
        Vector3 randomPoint = new Vector3(transform.position.x + axisX, transform.position.y + 1, transform.position.z + axisZ);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 3.0f, NavMesh.AllAreas))
        {
            int randomNPC = Random.Range(0, 21);
            if(randomNPC <= border1)
            {
                Instantiate(NPC[0], hit.position, Quaternion.identity);
            }
            else if(randomNPC <= border2)
            {
                Instantiate(NPC[1], hit.position, Quaternion.identity);
            }
            else
            {
                Instantiate(NPC[2], hit.position, Quaternion.identity);
            }
            
            canSpawn = false;
            Invoke(nameof(SetSpawnTrue), spawnDelay);
        }
    }

    private void SetSpawnTrue()
    {
        canSpawn = true;
    }
}
