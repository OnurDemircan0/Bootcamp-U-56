using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEffect : MonoBehaviour, IPooledObject
{
    private Rigidbody _rigidBody;

    [SerializeField]
    float trailSpeed = 10f;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    public void OnObjectSpawn()
    {
        _rigidBody.velocity = transform.forward * trailSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
    }
}
