using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXEplosionPlayOnLoop : MonoBehaviour, IPooledObject
{
    [SerializeField] ParticleSystem ParticleSystem;

    [SerializeField] ParticleSystem ParticleSystem1;

    [SerializeField] ParticleSystem ParticleSystem2;

    [SerializeField] ParticleSystem ParticleSystem3;

    [SerializeField] float setFalseTime = 2f;


    public void OnObjectSpawn()
    {
        ParticleSystem.Play();
        ParticleSystem1.Play();
        ParticleSystem2.Play();
        ParticleSystem3.Play();

        Invoke("SetThisFalse", 2f);
    }

    void SetThisFalse()
    {
        this.gameObject.SetActive(false);
    }
}
