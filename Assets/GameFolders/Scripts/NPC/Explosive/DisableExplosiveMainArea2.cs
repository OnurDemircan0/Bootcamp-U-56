using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExplosiveMainArea2 : MonoBehaviour
{
    private float objectScale;
    private bool doOnce;

    private void Start()
    {
        doOnce = true;
    }
    private void Update()
    {
        objectScale = transform.localScale.x;

        if(objectScale <= 50)
        {
            if (doOnce)
            {
                VirusCount.virusCount++;
                LastVirusCount.lastVirusCount++;
                Destroy(transform.parent.gameObject);
            }
            
        }
    }
}
