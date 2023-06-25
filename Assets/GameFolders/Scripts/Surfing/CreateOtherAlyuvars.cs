using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherAlyuvars : MonoBehaviour
{
    [SerializeField] private GameObject otherAlyuvars;

    [SerializeField] private int otherAlyuvarsCount;

    void Start()
    {
        for(int i=0; i < otherAlyuvarsCount; i++)
        {
            Instantiate(otherAlyuvars, transform.position, transform.rotation);

        }
    }

}
