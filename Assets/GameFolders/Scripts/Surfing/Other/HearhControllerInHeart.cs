using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearhControllerInHeart : MonoBehaviour
{
    [SerializeField] private Death death;
    [SerializeField] private HeartIconShowController heartIconShowController;


    void Awake()
    {
        InvokeRepeating("checkHealth", 0.1f, 0.05f);
    }


    private void checkHealth()
    {
        heartIconShowController.setHealth((int)death.health);
    }


}
