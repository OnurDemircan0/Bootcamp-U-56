using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaNPC : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<NPC_Spawning>().enabled = true;
        }
    }
}
