using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCheckingArea : MonoBehaviour
{
    public static bool playerDetectedMainVirusArea;

    private void Start()
    {
        playerDetectedMainVirusArea = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetectedMainVirusArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetectedMainVirusArea = false;
        }
    }
}
