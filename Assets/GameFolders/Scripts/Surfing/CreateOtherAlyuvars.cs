using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherAlyuvars : MonoBehaviour
{
    [SerializeField] private GameObject otherAlyuvars;

    [SerializeField] private int otherAlyuvarsCount;

    [SerializeField] private bool createAlyuvarControl;

    void Start()
    {
        //createAlyuvars();
    }

    private void createAlyuvars()
    {
        for (int i = 0; i < otherAlyuvarsCount; i++)
        {
            Instantiate(otherAlyuvars, transform.position, transform.rotation);
        }
    }


    private void Update()
    {
        if (createAlyuvarControl)
        {
            createAlyuvars();
            createAlyuvarControl = false;
        }
    }

}
