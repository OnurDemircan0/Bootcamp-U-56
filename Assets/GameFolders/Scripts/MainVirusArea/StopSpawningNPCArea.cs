using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpawningNPCArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<NPC_Spawning>().enabled = false;
        }
    }
}
