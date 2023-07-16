using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExplosive : MonoBehaviour
{
    private float objectScale;

    private void Update()
    {
        objectScale = transform.localScale.x;

        if(objectScale <= 50)
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
