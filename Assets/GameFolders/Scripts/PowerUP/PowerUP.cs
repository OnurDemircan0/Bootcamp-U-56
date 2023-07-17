using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour
{
    [SerializeField]
    Death playerHealthScript;

    [SerializeField]
    GameObject healthUPEffect;

    [SerializeField]
    Transform MedBOT;

    [SerializeField]
    AudioSource powerUPAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            PickUP();
        }
    }

    void PickUP()
    {
        playerHealthScript.health = 100f;

        powerUPAudio.Play();    

        Destroy(healthUPEffect.gameObject);
        Destroy(gameObject);
    }
}
