using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExplosiveMainArea : MonoBehaviour
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
                Destroy(transform.parent.gameObject);
            }
            
        }
    }
}
