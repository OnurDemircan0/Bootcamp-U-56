using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossVein : MonoBehaviour
{
    [SerializeField]
    DissolveObject dissolveObject;


    private void OnTriggerEnter(Collider other)
    {
        dissolveObject.insideBossVein = true;
    }
}
