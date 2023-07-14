using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDifferentObject : MonoBehaviour
{
    public Transform target;

    [SerializeField] private BossController bossController;

    private float speed;

    public float minSpeed;
    public float maxSpeed;


    [SerializeField] private float minFarForDestroyX;
    [SerializeField] private float minFarForDestroyY;
    [SerializeField] private float minFarForDestroyZ;

    private void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);
    }

    void Update()
    {
        Vector3 direction = target.position - transform.position;

        if (Mathf.Abs(direction.x) <= minFarForDestroyX && Mathf.Abs(direction.y) <= minFarForDestroyY && Mathf.Abs(direction.z) <= minFarForDestroyZ)
        {
            //Debug.Log("Hedefe Ulaþýldý X: " + Mathf.Abs(direction.x).ToString());
            //Debug.Log("Hedefe Ulaþýldý Y: " + Mathf.Abs(direction.y).ToString());
            //Debug.Log("Hedefe Ulaþýldý Z: " + Mathf.Abs(direction.z).ToString());

            bossController.magnificationBoss();

            Destroy(gameObject);
        }

        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;
    }
}
