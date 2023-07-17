using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBossVein : MonoBehaviour
{
    [SerializeField]
    DissolveObject dissolveObject;

    public bool enteredBossVein;

    private void OnTriggerEnter(Collider other)
    {
        dissolveObject.insideBossVein = true;
        enteredBossVein = true;
    }
}
