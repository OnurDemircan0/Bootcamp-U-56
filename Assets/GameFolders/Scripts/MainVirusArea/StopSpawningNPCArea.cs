using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpawningNPCArea : MonoBehaviour
{
    public static bool stopSpawningNPC;

    private void Start()
    {
        stopSpawningNPC = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stopSpawningNPC = true;
        }
    }
}
