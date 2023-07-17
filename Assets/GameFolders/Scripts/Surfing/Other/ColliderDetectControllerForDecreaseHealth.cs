using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDetectControllerForDecreaseHealth : MonoBehaviour
{

    private HeartIconShowController heartIconShowController;
    private Death death;

    [SerializeField] private float decreaseHealthCount;



    void Awake()
    {
        Debug.Log("ColliderDetectControllerForDecreaseHealth çalýþtý");

        heartIconShowController = GameObject.Find("GameManager").GetComponent<HeartIconShowController>();
        death = GameObject.Find("MedBOT").GetComponent<Death>();
    }




    private void decreaseHealth()
    {
        heartIconShowController.decreaseHealth(decreaseHealthCount);
        
        death.health -= decreaseHealthCount;

        if(death.health == 0)
        {
            death.health = -1;
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ColliderDetectControllerForDecreaseHealth saðlýk düþtü");

        if (other.gameObject.CompareTag("Player"))
        {
            decreaseHealth();
        }
    }
}
