using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.UI;


public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float AimSensitivity;
    [SerializeField] float NormalSensitivity;
    [SerializeField] RawImage CrossHair;
    [SerializeField] LayerMask AimRayLayerMask;
    [SerializeField] GameObject RayCastDebug;
    [SerializeField] GameObject pfBulletProjectile;
    [SerializeField] GameObject bulletInstantiateLocation;
    [SerializeField] GameObject VFXhitTarget;
    [SerializeField] GameObject VFXhitOther;

    [SerializeField] private float interpolationDuration = 0.3f;
    private float interpolationTimer = 0f;

    StarterAssetsInputs assetsInputs;
    ThirdPersonController thirdPerson;

    bool AimWalkTriggered = false;
    bool AimSprintTriggered = false;
    bool ýsAiming;
    bool OnWalk;
    bool OnSprint;
    bool AimIdleTriggered;

    Animator animator;
    private void Start()
    {
        assetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPerson = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Vector3 WorldAimPoint = Vector3.zero;

        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Transform hitTransform = null;
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimRayLayerMask))
        {
            WorldAimPoint = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        if (assetsInputs.aim)
        {
            thirdPerson.SetRotateAim(false);
            virtualCamera.gameObject.SetActive(true);
            thirdPerson.SetSensitivity(AimSensitivity);
            CrossHair.gameObject.SetActive(true);

            Vector3 AimTarget = WorldAimPoint;
            AimTarget.y = transform.position.y;
            Vector3 AimDirection = (AimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, AimDirection, Time.deltaTime * 20f);

            ýsAiming = true;

            AýmAnim();
        }


        else
        {
            thirdPerson.SetRotateAim(true);
            virtualCamera.gameObject.SetActive(false);
            thirdPerson.SetSensitivity(NormalSensitivity);
            CrossHair.gameObject.SetActive(false);

            ýsAiming = false;

            AýmAnim();
        }

        //Mermi Kullanmadan Anýnda Zarar Ýçin

        if(assetsInputs.shoot)
        {
            animator.SetBool("Shooting", true);
            if(hitTransform != null)
            {
                if (hitTransform.transform.GetComponent<Hit_Target>() != null)
                {
                    Instantiate(VFXhitTarget, raycastHit.point , Quaternion.identity);
                }
                else
                {
                    Instantiate(VFXhitOther, raycastHit.point , Quaternion.identity);
                    AimIdleTriggered = true;
                }
            }
            
        }
        else
        {
            animator.SetBool("Shooting", false);
        }

        // Silahýn Prjectile Atmasý Ýçin

        //if(assetsInputs.shoot)
        //{
        //    Vector3 bulletDirection = (WorldAimPoint - bulletInstantiateLocation.transform.position).normalized;
        //
        //    Instantiate(pfBulletProjectile, bulletInstantiateLocation.transform.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
        //    assetsInputs.shoot = false;
        //}

        void AimJump()
        {
            if (assetsInputs.jump)
            {
                animator.SetBool("Jump Aim", true);
               // animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
            }
            else if(!assetsInputs.jump)
            {
                animator.SetBool("Jump Aim", false);
               // animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
            }
            
        }
        void AýmAnim()
        {
            if(ýsAiming)
            {
                AimJump();
                thirdPerson.IsAim = true;
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 10f));
                if (assetsInputs.move.x == 0f && assetsInputs.move.y == 0f)
                {
                    OnWalk = false;
                    OnSprint = false;
                    animator.SetBool("ToMove", false);
                    animator.SetBool("WalkBackAim", false);

                    if (AimWalkTriggered && !OnWalk)
                    {
                        ReturnToIdle();
                    }
                    else if (AimSprintTriggered)
                    {
                        ReturnToIdleFromSprint();
                    }
                    else
                    {
                        animator.SetFloat("Speed", 0f);
                        AimIdleTriggered = true;

                    }
                }
                else if (assetsInputs.sprint && (assetsInputs.move.x > 0f || assetsInputs.move.y > 0f) && !AimSprintTriggered && AimIdleTriggered && !OnWalk && !OnSprint && !AimWalkTriggered && thirdPerson.SprintSpeed == 5.335f)
                {
                    animator.SetBool("WalkBackAim", false);
                    animator.SetBool("ToMove", false);
                    Debug.Log("Here");
                    SprintAimAnim();
                }
                else if (assetsInputs.move.y > 0f)
                {
                    animator.SetBool("WalkBackAim", false);
                    animator.SetBool("ToMove", false);
                    OnWalk = true;
                    if (assetsInputs.sprint && thirdPerson.SprintSpeed == 5.335f)
                    {
                        OnSprint = true;

                        if (OnWalk && !AimSprintTriggered)
                        {
                            SprintAimAnimOnWalk();
                        }
                        else
                        {
                            TriggeredAimSprint();
                        }
                    }
                    else if (AimSprintTriggered && OnWalk)
                    {
                        ReturnToWalk();
                    }

                    else if (!AimWalkTriggered && AimIdleTriggered)
                    {
                        WalkAimAnim();
                    }
                    else
                    {
                        TriggeredAimWalk();
                    }
                }
                else if(assetsInputs.move.y < 0f)
                {
                    animator.SetBool("WalkBackAim", true);
                    animator.SetBool("ToMove", false);
                }
                else if (assetsInputs.move.x < 0f || assetsInputs.move.x > 0f)
                {
                    animator.SetBool("ToMove", true);
                    animator.SetBool("WalkBackAim", false);
                    if (assetsInputs.move.x < 0f)
                    {
                        animator.SetFloat("AimMove", -1f);
                    }
                    else
                    {
                        animator.SetFloat("AimMove", 1f);
                    }

                }
            }
            else
            {
                AimSprintTriggered = false;
                OnWalk = false;
                AimWalkTriggered = false;
                thirdPerson.IsAim = false;
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f , Time.deltaTime * 10f));
            }
           
        }

        void TriggeredAimSprint()
        {
            animator.SetFloat("Speed", 1f);
        }

        void SprintAimAnimOnWalk()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / interpolationDuration);

            animator.SetFloat("Speed", Mathf.Lerp(0.66f, 1f, t));

            if (t >= 1f)
            {
                AimSprintTriggered = true;
                interpolationTimer = 0f;
                AimWalkTriggered = false;
                AimIdleTriggered = false;
            }
        }

        void SprintAimAnim()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration + 0.1f));

            animator.SetFloat("Speed", Mathf.Lerp(0f, 1f, t));
            

            if (t >= 1f)
            {
                AimSprintTriggered = true;
                interpolationTimer = 0f;
                AimWalkTriggered = false;
                AimIdleTriggered = false;
            }
        }

        void WalkAimAnim()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / interpolationDuration);

            animator.SetFloat("Speed", Mathf.Lerp(0f, 0.66f, t));

            if(t >= 1f)
            {
                interpolationTimer = 0f;

                AimWalkTriggered = true;
                AimSprintTriggered = false;
                AimIdleTriggered = false;
            }
            
        }

        void ReturnToWalk()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / interpolationDuration);

            animator.SetFloat("Speed", Mathf.Lerp(1f, 0.66f, t));

            if(t >= 1f)
            {
                interpolationTimer = 0f;

                AimWalkTriggered = true;
                AimSprintTriggered = false;
            }


        }

        void ReturnToIdleFromSprint()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration + 0.1f));

            animator.SetFloat("Speed", Mathf.Lerp(1f, 0f, t));

            if (t >= 1f)
            {
                interpolationTimer = 0f;

                AimWalkTriggered = false;
                AimSprintTriggered = false;
                AimIdleTriggered = true;

            }
        }

        void ReturnToIdle()
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / interpolationDuration);

            animator.SetFloat("Speed", Mathf.Lerp(0.66f, 0f, t));

            if(t >= 1f)
            {
                interpolationTimer = 0f;

                AimWalkTriggered = false;
                AimSprintTriggered = false;
                AimIdleTriggered = true;

            }
        }
        void TriggeredAimWalk()
        {
            animator.SetFloat("Speed", 0.66f);
        }
    }

}
