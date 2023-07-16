using MimicSpace;
using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using EZCameraShake;

public class Death : MonoBehaviour
{
    public bool death = false;
    bool onAim;
    bool onShoot;

    public string scene;

    public bool onMimicStage = false;

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

    [SerializeField]
    Movement mimicMovement;

    [SerializeField]
    GameObject muzzle;

    [SerializeField]
    AudioSource gunSound;

    [SerializeField]
    GameObject CrossHair;

    [SerializeField]
    AudioSource burnOutMeterSound;

    [SerializeField]
    PlayerInput playerInput;
    
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
            if (muzzle != null && gunSound != null)
            {
                muzzle.SetActive(false);
                gunSound.Stop();
            }

            if (deathCam != null) {
                deathCam.SetActive(true);
                StartCoroutine(DeathAnimation());
            }
            else
            {
                Debug.LogWarning("Death Cam is not assigned");
            }
            
            if(disableScripts != null)
            {
                disableScripts.DisableAllScripts();
            }
            else
            {
                Debug.LogWarning("Disable Script is not assigned");
            }
            if (muzzle != null) muzzle.SetActive(false);
            Invoke("Fade", 5f);
            Invoke("ResetLevel", 10f);

            if(burnOutMeterSound != null)
            {
                burnOutMeterSound.Stop();
            }

            if(playerInput != null)
            {
                playerInput.enabled = false;
            }

            if(CrossHair != null) 
            {
                CrossHair.SetActive(false);
            }
        }
    }

    void DeathActive()
    {
        if(health < 0f)
        {
            death = true;
            if(muzzle != null && gunSound != null)
            {
                muzzle.SetActive(false);
                gunSound.Stop();
            }
            
            if(deathCam != null)
            {
                deathCam.SetActive(true);
                StartCoroutine(DeathAnimation());
            }
            else
            {
                Debug.LogWarning("Death Cam is not assigned");
            }
            
            if(disableScripts != null)
            {
                disableScripts.DisableAllScripts();
            }
            else
            {
                Debug.LogWarning("Disable Script is not assigned");
            }
            
            if(muzzle != null) muzzle.SetActive(false );
            Invoke("Fade", 5f);
            Invoke("ResetLevel", 10f);

            if (burnOutMeterSound != null)
            {
                burnOutMeterSound.Stop();
            }

            if (playerInput != null)
            {
                playerInput.enabled = false;
            }

            if (CrossHair != null)
            {
                CrossHair.SetActive(false);
            }

        }
    }

    private void LateUpdate()
    {
        DeathActive();
    }


    IEnumerator DeathAnimation() 
    {if(animator != null)
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
    else
        {
            Debug.LogWarning("Animator is not assigned");
        }
    }
        

    void Fade()
    {
        if(fadeCam != null)
        {
            fadeCam.isDead = true;
        }
        else
        {
            Debug.LogWarning("Fade Cam is not assigned");
        }
    }

    public void TakeHit(int damage)
    {
        health -= damage;

        AudioSource.PlayClipAtPoint(hitClip, transform.position);

        if(mimicMovement != null && onMimicStage)
        {
            mimicMovement.CheckPlayerHealth();
        }

        animator.SetTrigger("Hit");
        try
        {
            ShakeHit();
        }
        catch
        {
            Debug.LogWarning("Shake Method Couldn't Execute --- Check if ShakeManager is Inside the Scene");
        }
    }

    void ShakeHit()
    {
        if (CinemachineCameraShaker.Instance != null)
        {
            //Debug.Log("Shake Hit Activated");
            CinemachineCameraShaker.Instance.ShakeCameraFollow(0.3f, .3f, true);
            CinemachineCameraShaker.Instance.ShakeCameraAim(0.3f, .3f, true);
            //Debug.Log("Shake Hit Finished");
        }
        else
        {
            throw new System.Exception("ShakeManager is not present");
        }
    }

    void ResetLevel()
    {
        SceneManager.LoadScene(scene);
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
