using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXPlayOnPool : MonoBehaviour, IPooledObject
{               
    private ParticleSystem _particleSystem;
    bool set = false;
    public void OnObjectSpawn()
    {
        if(!set)
        {
            _particleSystem = GetComponent<ParticleSystem>();
            set = true;
        }
        _particleSystem.Play();
    }
}
