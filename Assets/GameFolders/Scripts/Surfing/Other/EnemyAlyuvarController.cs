using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlyuvarController : MonoBehaviour
{
    private float speed;

    public float minSpeed;
    public float maxSpeed;

    [SerializeField] private HeartIconShowController heartIconShowController;

    [SerializeField] private GameObject enemyAlyuvarForwardDirection;

    [SerializeField] private float damageCountEveryTrigger;

    private Vector3 bossEnemyVirusTransformForward;

    private void Awake()
    {
        bossEnemyVirusTransformForward = enemyAlyuvarForwardDirection.transform.forward;
    }

    void Start()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        heartIconShowController = GameObject.Find("GameManager").gameObject.GetComponent<HeartIconShowController>();
        enemyAlyuvarForwardDirection = GameObject.Find("Enemy Atack Show Way").gameObject;

        Destroy(gameObject, 20);
    }


    void Update()
    {
        //transform.position += Vector3.forward * speed * Time.deltaTime * -1;

        transform.position += bossEnemyVirusTransformForward * speed * Time.deltaTime * -1;
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("çarpýþan Nesnenin Ýsmi: " + other.gameObject.name);

        if (other.gameObject.name.Contains("MedBOT"))
        {
            heartIconShowController.decreaseHealth(damageCountEveryTrigger);

            Debug.Log("çarpýþan Nesne Robot");
        }
    }
}
