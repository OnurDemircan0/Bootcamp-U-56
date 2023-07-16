using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlideV2 : MonoBehaviour
{
    MonoBehaviour[] Scripts;

    CharacterController characterController;

    Animator animator;

    bool pressed = false;

    bool lockedCamera;

    [SerializeField]
    GameObject slideChecker;

    [SerializeField]
    float checkRadius = 0.75f;

    [SerializeField]
    LayerMask slideLayer;

    [SerializeField]
    float horizontalSpeed = 2f;

    [SerializeField]
    float speed = 5f;

    [SerializeField]
    ThirdPersonShooterController shooterController;

    [SerializeField]
    MuzzleEffect muzzleEffect;

    [SerializeField]
    StarterAssetsInputs assetsInputs;

    PlayerInput playerInput;

    ThirdPersonController thirdPersonController;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(slideChecker.transform.position, checkRadius);
    }
    private void Start()
    {
        Scripts = GetComponents<MonoBehaviour>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void FixedUpdate()
    {
        bool onSlide = Physics.CheckSphere(slideChecker.transform.position, checkRadius, slideLayer);

        if (onSlide)
        {
            StartSliding();
        }
        else
        {
            StopSliding();
        }
    }

    private void Update()
    {
      //  assetsInputs.move.x = 0f;
        assetsInputs.shoot = false;
        assetsInputs.aim = false;
      //  if (!lockedCamera) { thirdPersonController.LockCameraPosition = true; lockedCamera = true; }
      //  if (pressed) playerInput.enabled = false;

    }

    void StartSliding()
    {
        Invoke("pressW", 0.6f);

        // Disables

        if(pressed)
        {
           // assetsInputs.enabled = false;
           // playerInput.enabled = false;
           // thirdPersonController.enabled = false;  
        }



        animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 1f, Time.deltaTime * 10f));

        transform.rotation = Quaternion.Euler(0f, transform.rotation.y - 90f, 0f);

        Vector3 forwardDirection = new Vector3(0f, 0f, -1f).normalized;

        shooterController.enabled = false;
        muzzleEffect.enabled = false;

        Vector3 moveVector = forwardDirection * speed * Time.deltaTime;
        characterController.Move(moveVector);

        Invoke("stopSlidingNoMatterWhat", 4.35f);
    }

    void StopSliding()
    {
        animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 0f, Time.deltaTime * 10f));
        shooterController.enabled = true;
        muzzleEffect.enabled = true;
    }

    void pressW()
    {
        if(!pressed)
        {
            assetsInputs.move.y = 1f;
            pressed = true;
        }
    }

    void stopSlidingNoMatterWhat()
    {
        animator.SetLayerWeight(4, Mathf.Lerp(animator.GetLayerWeight(4), 0f, Time.deltaTime * 10f));
        shooterController.enabled = true;
        muzzleEffect.enabled = true;
        playerInput.enabled = true;
        thirdPersonController.LockCameraPosition = false;

        this.enabled = false;
    }

   // public void DisableAllScripts()
   // {
   //     foreach (MonoBehaviour script in Scripts)
   //     {
   //         if (script == this || script == personController) { continue; }
   //
   //         else
   //         {
   //             script.enabled = false;
   //         }
   //     }
   // }
   //
   // public void EnableAllScripts()
   // {
   //     foreach (MonoBehaviour script in Scripts)
   //     {
   //         if (script == this) { this.enabled = false; continue; }
   //
   //         else
   //         {
   //             script.enabled = true;
   //         }
   //     }
   // }
}

