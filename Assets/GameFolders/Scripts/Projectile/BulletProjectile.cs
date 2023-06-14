using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] Transform VFXhitTarget;
    [SerializeField] Transform VFXhitOther;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        _rigidBody.velocity = transform.forward * 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Hit_Target>() != null)
        {
            Instantiate(VFXhitTarget, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(VFXhitOther, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}
