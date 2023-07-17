using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBotMedicineChecking : MonoBehaviour
{
    private bool doOnce = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (doOnce)
            {
                doOnce = false;
                ChatBotBrain.pass = true;
            }
        }
    }
}
