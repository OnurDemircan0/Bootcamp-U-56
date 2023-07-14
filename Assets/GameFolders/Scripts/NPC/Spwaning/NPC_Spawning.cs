using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_Spawning : MonoBehaviour
{
    [SerializeField] GameObject[] NPCs;
    [SerializeField] float range = 30.0f;
    [SerializeField] float spawnDelay = 1f;

    private bool canSpawn;

    private void Start()
    {
        canSpawn = true;
    }

    private void Update()
    {
        if (canSpawn && !StopSpawningNPCArea.stopSpawningNPC)
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
            int randomNPC = Random.Range(0, 3);
            Instantiate(NPCs[randomNPC], hit.position, Quaternion.identity);
            canSpawn = false;
            Invoke(nameof(SetSpawnTrue), spawnDelay);
        }
    }

    private void SetSpawnTrue()
    {
        canSpawn = true;
    }
}
