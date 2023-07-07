using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ObjectPooler;

public class BulletProjectileEnemy : MonoBehaviour, IPooledObject
{
    public float bulletSpeed = 10f;
    public Rigidbody _rigidBody;
    public int bulletDamage = 12;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public  void OnObjectSpawn()
    {
        _rigidBody.velocity = transform.forward * bulletSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rigidBody.velocity = Vector3.zero;
            ObjectPooler.instance.SpawnFromPool("HitEffectEnemy", _rigidBody.position, Quaternion.identity);
            other.GetComponent<Death>().TakeHit(bulletDamage);
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
            ObjectPooler.instance.SpawnFromPool("OtherEffectEnemy", _rigidBody.position, Quaternion.identity);
        }
        this.gameObject.SetActive(false);
    }

}
