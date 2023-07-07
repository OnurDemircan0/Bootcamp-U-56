using MimicSpace;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using EZCameraShake;

public class Death : MonoBehaviour
{
    bool death;
    bool onAim;
    bool onShoot;

    [SerializeField] 
    GameObject deathCam;

    [SerializeField] 
    Animator animator;

    [SerializeField]
    DisableScripts disableScripts;

    [SerializeField] 
    public float health = 100f;

    [SerializeField]
    FadeCamera fadeCam;

    [SerializeField]
    AudioClip hitClip;

    //public CameraShaker cameraShaker;

    ThirdPersonController personController;
    ThirdPersonShooterController shooterController;


    Color currentColor;

    //ThirdPersonController personController;
    //[SerializeField] LayerMask BloodBottomLayer;

    private void Start()
    {
        personController = GetComponent<ThirdPersonController>();
        shooterController = GetComponent<ThirdPersonShooterController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            deathCam.SetActive(true);
            StartCoroutine(DeathAnimation());
            disableScripts.DisableAllScripts();
        }
    }

    void DeathActive()
    {
        if(health < 0f)
        {
            deathCam.SetActive(true);
            StartCoroutine(DeathAnimation());
            disableScripts.DisableAllScripts();

            Invoke("Fade", 5f);
        }
    }

    private void LateUpdate()
    {
        DeathActive();
    }


    IEnumerator DeathAnimation() 
    {
        float timer = 0f;
        float duration = 1f;
        float startWeight = animator.GetLayerWeight(5);
        float targetWeight = 1.0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float currentWeight = Mathf.Lerp(startWeight, targetWeight, t);
            animator.SetLayerWeight(5, currentWeight);
            yield return null;
        }

        animator.SetLayerWeight(5, targetWeight);
        yield break;
    }

    void Fade()
    {
        fadeCam.isDead = true;
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        AudioSource.PlayClipAtPoint(hitClip, transform.position);

        animator.SetTrigger("Hit");

        ShakeHit();

    }

    void ShakeHit()
    {
        Debug.Log("Shake Hit Activated");
        CinemachineCameraShaker.Instance.ShakeCameraFollow(0.3f, .3f, true);
        CinemachineCameraShaker.Instance.ShakeCameraAim(0.3f, .3f, true);
        Debug.Log("Shake Hit Finished");
    }


   //void Die()
   //{
   //    Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - personController.GroundedOffset,
   //            transform.position.z);
   //    death = Physics.CheckSphere(spherePosition, personController.GroundedRadius, BloodBottomLayer,
   //        QueryTriggerInteraction.Ignore);
   //
   //    if(death)
   //    {
   //        animator.SetLayerWeight(5, Mathf.Lerp(animator.GetLayerWeight(5), 1f, Time.deltaTime * 10f));
   //        deathCam.SetActive(true);
   //    }
   //}
}
