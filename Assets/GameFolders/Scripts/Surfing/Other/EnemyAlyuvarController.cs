using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAlyuvarController : MonoBehaviour
{
    private float speed;

    public float minSpeed;
    public float maxSpeed;

    [SerializeField] private HeartIconShowController heartIconShowController;

    [SerializeField] private SurfingControllerV2 surfingControllerV2;

    [SerializeField] private CameraShakeControllerInVein cameraShakeControllerInVein;

    [SerializeField] private AudioSource surfingAlyuvarAudioSource;
    [SerializeField] private AudioClip shockwaveSound;

    [SerializeField] private GameObject enemyAlyuvarForwardDirection;

    [SerializeField] private float damageCountEveryTrigger;

    [SerializeField] private float cameraShakeIntensityForEnemyAlyuvarAtack;
    [SerializeField] private float cameraShakefullIntensityTimeForEnemyAlyuvarAtack;
    [SerializeField] private float cameraShakeGoToZeroTimeForEnemyAlyuvarAtack;
    [SerializeField] private GameObject shockwave;
    [SerializeField] private GameObject shockwave2;

    public bool destroyAndExplodeControl = false;

    private Vector3 bossEnemyVirusTransformForward;

    private void Awake()
    {
        speed = Random.Range(minSpeed, maxSpeed);

        heartIconShowController = GameObject.Find("GameManager").gameObject.GetComponent<HeartIconShowController>();
        surfingControllerV2 = GameObject.Find("Surf Alyuvar 2").gameObject.GetComponent<SurfingControllerV2>();
        surfingAlyuvarAudioSource = surfingControllerV2.gameObject.GetComponent<AudioSource>();
        cameraShakeControllerInVein = GameObject.Find("GameManager").gameObject.GetComponent<CameraShakeControllerInVein>();
        enemyAlyuvarForwardDirection = GameObject.Find("Enemy Atack Show Way").gameObject;

        //Destroy(gameObject, 20);

        bossEnemyVirusTransformForward = enemyAlyuvarForwardDirection.transform.forward;
    }



    void Update()
    {
        //transform.position += Vector3.forward * speed * Time.deltaTime * -1;

        transform.position += bossEnemyVirusTransformForward * speed * Time.deltaTime * -1;

        if (destroyAndExplodeControl)
        {
            destroyAndExplode();
        }
    }

    public void destroyAndExplode(float waitTimeForExplosion = 0)
    {
        Invoke("destroyAndExplodeNow", waitTimeForExplosion);
    }

    private void destroyAndExplodeNow()
    {
        surfingAlyuvarAudioSource.PlayOneShot(shockwaveSound);
        Instantiate(shockwave2, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject, 0.1f);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("çarpýþan Nesnenin Ýsmi: " + other.gameObject.name);

        if (other.gameObject.name.Contains("MedBOT"))
        {
            heartIconShowController.decreaseHealth(damageCountEveryTrigger);

            cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForEnemyAlyuvarAtack, cameraShakefullIntensityTimeForEnemyAlyuvarAtack, cameraShakeGoToZeroTimeForEnemyAlyuvarAtack);
            //shockwave.SetActive(true);
            surfingAlyuvarAudioSource.PlayOneShot(shockwaveSound);
            Instantiate(shockwave2, surfingControllerV2.gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject,0.1f);


            Debug.Log("çarpýþan Nesne Robot");
        }

        if (other.gameObject.name.Contains("Destroy Enemy"))
        {
            Destroy(gameObject, 0.1f);
        }
    }
}
