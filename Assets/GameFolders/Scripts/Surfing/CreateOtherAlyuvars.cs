using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherAlyuvars : MonoBehaviour
{
    [SerializeField] private GameObject otherAlyuvars;

    [SerializeField] private int otherAlyuvarsCount;

    // Start is called before the first frame update
    void Start()
    {
        for(int i=0; i < otherAlyuvarsCount; i++)
        {
            Instantiate(otherAlyuvars, transform.position, transform.rotation);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
