using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticleFalse : MonoBehaviour
{
    ParticleSystem thisSystem;

    [SerializeField]
    float particleSetFalseTime = 2f;
    void Start()
    {
        thisSystem = GetComponent<ParticleSystem>();

        Invoke("SetToFalse", particleSetFalseTime);
    }


    void SetToFalse()
    {
        thisSystem.gameObject.SetActive(false);
    }

}
