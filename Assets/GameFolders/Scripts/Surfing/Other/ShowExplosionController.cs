using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowExplosionController : MonoBehaviour
{
    [SerializeField] private GameObject shockWave;


    void Start()
    {
        
    }


    private void createShockWave()
    {
        Instantiate(shockWave, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("other.name: " + other.name);
        Debug.Log("other.name.Contains(Enemy Alyuvar): " + other.name.Contains("Enemy Alyuvar"));
        Debug.Log("other.name.Contains(MedBOT): " + other.name.Contains("MedBOT"));

        if (other.name.Contains("MedBOT"))
        {
            Debug.Log("other.name: bomba patladý");
            //createShockWave();
        }
        
    }

}
