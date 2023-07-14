using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandBYTrigger : MonoBehaviour
{
    [SerializeField]
    GetEnemies getEnemies;

    private bool hasPlayerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasPlayerEntered)
        {
            hasPlayerEntered = true;
            getEnemies.enteredStandBy = true;
        }
    }
}