using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.UI;

public class ThirdPersonShooterController : MonoBehaviour
{
    [Range(-100f, 100f)]
    public float rotationPercentage;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] float AimSensitivity;
    [SerializeField] float NormalSensitivity;
    [SerializeField] RawImage CrossHair;
    [SerializeField] LayerMask AimRayLayerMask;
    [SerializeField] GameObject RayCastDebug;
    [SerializeField] GameObject RayCastDebugChild;
    [SerializeField] GameObject pfBulletProjectile;
    [SerializeField] GameObject trailSpawnLocation;
    [SerializeField] GameObject VFXhitTarget;
    [SerializeField] GameObject VFXhitOther;
    [SerializeField] Transform muzzleAim;
    [SerializeField] float bulletDamage = 8.2f;

    [SerializeField] int DesiredInstanceAmount = 3;

    [SerializeField] private float interpolationDuration = 0.3f;

    [SerializeField] private float interpolationDuration2 = 0.15f;

    
    public float burnOutMeter = 0f;
    [SerializeField] CameraShakeManager cameraShakeManager;
    public float burnOutLimit = 100f;
    public float burnOutTime = 10f;
    [SerializeField] float burnOutRate = 5.4f;
    [SerializeField] float afterBurntOutWait = 2.8f;
    float timerForBurntOut = 0f;

    bool OnResetBurnOut = false;
    bool triggeredSetTimer = false;
    bool burnedOutEngaged = false;
    bool triggeredBurNoutShoot = false;

    [HideInInspector]
    public bool burnedOut = false;

    private float interpolationTimer = 0f;

    StarterAssetsInputs assetsInputs;
    ThirdPersonController thirdPerson;

    public bool EnemyHit;
    bool AimWalkTriggered = false;
    bool AimSprintTriggered = false;
    public bool ısAiming;
    bool OnWalk;
    bool OnSprint;
    bool AimIdleTriggered;
    bool IK_on = false;
    public bool ısShooting = false;

    int c = 0;
    int co = 0;

    Animator animator;

    AimIK aimIK;

    GameObject[] TargetInstances = null;
    GameObject[] OtherHitInstances = null;

    private Transform CharacterTransform;

    public float rotationSpeed = 5f;
    bool functionInProgress = false;
    private void Start()
    {
        assetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPerson = GetComponent<ThirdPersonController>();
        animator = GetComponent<Animator>();
        aimIK = GetComponent<AimIK>();
        CharacterTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if(ısAiming || ısShooting)
        {
            SetIkWeightON();
        }
        else
        {
            SetIkWeightOFF();
        }
        burnOutMeter = Mathf.Clamp(burnOutMeter, 0f, burnOutLimit + 5);
       if(!ısShooting && !OnResetBurnOut) burnOutMeter = Mathf.Lerp(burnOutMeter, 0f, Time.deltaTime * burnOutTime);

        if (burnOutMeter >= burnOutLimit)
        {
            if (!triggeredSetTimer)
            {
                timerForBurntOut = afterBurntOutWait;
                triggeredSetTimer = true;
            }
            timerForBurntOut -= Time.deltaTime;

            if (timerForBurntOut > 0f)
            {
                burnedOut = true;
                OnResetBurnOut = true;
            }
            else
            {
                burnOutMeter = 0f;
                timerForBurntOut = 0f;
                OnResetBurnOut = false;
                triggeredSetTimer = false;
                burnedOut = false;
            }
        }

        
        if(burnedOut)
        {
            if (cameraShakeManager != null)
            {
                cameraShakeManager.burnedOut = true;
            }
        }
        else
        {
            if (cameraShakeManager != null)
            {
                cameraShakeManager.burnedOut = false;
            }
        }

        Vector3 WorldAimPoint = Vector3.zero;

        Vector2 ScreenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        Transform hitTransform = null;
        Ray ray = Camera.main.ScreenPointToRay(ScreenCenter);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, AimRayLayerMask))
        {
            WorldAimPoint = raycastHit.point;
            hitTransform = raycastHit.transform;
            RayCastDebug.transform.position = raycastHit.point;
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

            ısAiming = true;

            AımAnim();

        }


        else
        {
            thirdPerson.SetRotateAim(true);
            virtualCamera.gameObject.SetActive(false);
            thirdPerson.SetSensitivity(NormalSensitivity);
            CrossHair.gameObject.SetActive(false);

            ısAiming = false;

            AımAnim();
        }

        //Mermi Kullanmadan Anında Zarar İçin

        if(assetsInputs.shoot)
        {
            if(StopShooting.Instance != null && StopShooting.Instance.turnOffShoot)
            {

            }
            else
            {
                if(!burnedOut)
                {


                    ısShooting = true;
                    animator.SetBool("Shooting", true);
                    RotateToBack(RayCastDebug.transform, 80f);

                    burnOutMeter += burnOutRate * Time.deltaTime;
                    burnedOutEngaged = false;
                    animator.SetLayerWeight(6, Mathf.Lerp(animator.GetLayerWeight(6), 0f, Time.deltaTime * 10f));
                    animator.SetBool("burnedOut", false);
                    animator.SetBool("burnOutShoot", false);


                    if (hitTransform != null)
                    {
                        //Hasan Ekledi --------------------------------------------------------------------------------------------------------------------------------
                        if (hitTransform.gameObject.GetComponent<EnemyInVeinController>() != null)
                        {
                            EnemyInVeinController enemyInVeinController = hitTransform.transform.GetComponent<EnemyInVeinController>();

                            enemyInVeinController.enemyGetDamaged();
                            enemyInVeinController.hittedControl = true;
                        }
                        //---------------------------------------------------------------------------------------------------------------------------------------------


                        if (hitTransform.transform.GetComponent<Hit_Target>() != null)
                        {
                            if (hitTransform.transform.GetComponent<Enemy>() != null)
                            {
                                Enemy enemy = hitTransform.transform.GetComponent<Enemy>();
                                enemy.Health -= bulletDamage * Time.deltaTime;
                                enemy.correctEnemy = true;
                                EnemyHit = true;
                            }
                            else if (hitTransform.transform.GetComponent<mimicExplode>() != null)
                            {
                                WaitBeforeReady readyBloodPool = hitTransform.transform.GetComponent<WaitBeforeReady>();
                                readyBloodPool.triggeed = true;

                                if (readyBloodPool.isReady)
                                {
                                    ObjectPooler.instance.SpawnFromPool("mimicExplode", hitTransform.position, Quaternion.identity);
                                }

                            }

                            if ((TargetInstances == null || TargetInstances.Length < DesiredInstanceAmount))
                            {
                                Instantiate(VFXhitTarget, raycastHit.point, Quaternion.identity);
                                TargetInstances = GameObject.FindGameObjectsWithTag("VFXtarget");
                            }
                            else
                            {
                                if (c == (DesiredInstanceAmount))
                                {
                                    c = 0;
                                }

                                TargetInstances = GameObject.FindGameObjectsWithTag("VFXtarget");

                                TargetInstances[c].SetActive(false);

                                TargetInstances[c].transform.position = raycastHit.point;
                                TargetInstances[c].SetActive(true);
                                c++;
                            }
                        }
                        else
                        {
                            if ((OtherHitInstances == null || OtherHitInstances.Length < DesiredInstanceAmount))
                            {
                                Instantiate(VFXhitOther, raycastHit.point, Quaternion.identity);
                                OtherHitInstances = GameObject.FindGameObjectsWithTag("VFXother");
                            }
                            else
                            {
                                if (co == (DesiredInstanceAmount))
                                {
                                    co = 0;

                                }

                                OtherHitInstances = GameObject.FindGameObjectsWithTag("VFXother");

                                OtherHitInstances[co].SetActive(false);

                                OtherHitInstances[co].transform.position = raycastHit.point;
                                OtherHitInstances[co].SetActive(true);
                                co++;
                            }
                        }

                    }
                }
                else if (assetsInputs.shoot && burnedOut && burnedOutEngaged)
                {
                    animator.SetLayerWeight(6, Mathf.Lerp(animator.GetLayerWeight(6), 1f, Time.deltaTime * 10f));
                    animator.SetBool("burnedOut", false);
                    if(!triggeredBurNoutShoot)
                    {
                        animator.SetBool("burnOutShoot", true);
                        triggeredBurNoutShoot = true;
                    }
                    else
                    {
                        animator.SetBool("burnOutShoot", false);
                    }
                    
                    ısShooting = false;
                    animator.SetBool("Shooting", false);
                    EnemyHit = false;
                }
                else
                {
                    ısShooting = false;
                    animator.SetBool("Shooting", false);
                    EnemyHit = false;

                    animator.SetLayerWeight(6,Mathf.Lerp(animator.GetLayerWeight(6), 0f, Time.deltaTime * 10f));
                    animator.SetBool("burnedOut", true);

                    Invoke("SetEngageBurnOut", 0.125f);
                }
            }


            //if(hitTransform != null)
            //{
            //    if (hitTransform.transform.GetComponent<Hit_Target>() != null)
            //    {
            //        Instantiate(VFXhitTarget, raycastHit.point , Quaternion.identity);
            //    }
            //    else
            //    {
            //        Instantiate(VFXhitOther, raycastHit.point , Quaternion.identity);
            //        AimIdleTriggered = true;
            //    }
            //}

        }
        else
        {
            ısShooting = false;
            animator.SetBool("Shooting", false);
            EnemyHit = false;
            animator.SetBool("burnedOut", false);

            animator.SetBool("burnOutShoot", false);
            triggeredBurNoutShoot = false;
        }

        // Silahın Prjectile Atması İçin

        if(assetsInputs.shoot && trailSpawnLocation != null)
        {
            Vector3 bulletDirection = (WorldAimPoint - trailSpawnLocation.transform.position).normalized;
        
            ObjectPooler.instance.SpawnFromPool("TrailBulletPlayer", trailSpawnLocation.transform.position, Quaternion.LookRotation(bulletDirection, Vector3.up));
        }

        void AimJump()
        {
            if(StopMoving.Instance == null || !StopMoving.Instance.DisableMove)
            {
                if (assetsInputs.jump)
                {
                    animator.SetBool("Jump Aim", true);
                    // animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 1f, Time.deltaTime * 10f));
                }
                else if (!assetsInputs.jump)
                {
                    animator.SetBool("Jump Aim", false);
                    // animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
                }
            }            
        }
        void AımAnim()
        {
            if(ısAiming)
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
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration));

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
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration));

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
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration));

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
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration));

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

    void RotateToBack(Transform Target, float AngleLimit)
    {
        Vector3 CharacterDirection = CharacterTransform.forward;
        Vector3 TargetDirection = Target.position - CharacterTransform.position;

        float TargetAngle = Vector3.Angle(CharacterDirection, TargetDirection);
        if (TargetAngle > AngleLimit)
        {


            // if(Vector3.Cross(MuzzleDirection, TargetDirection).y >= 0)
            // {
            //     while(TargetAngle > 0)
            //     {
            //         float rotationAmount = rotationSpeed * Time.deltaTime;
            //         CharacterTransform.Rotate(Vector3.up, rotationAmount);
            //         TargetAngle = Vector3.Angle(MuzzleDirection, TargetDirection);
            //     }
            // }
            // else
            // {
            //     while(TargetAngle > 0)
            //     {
            //         float rotationAmount = rotationSpeed * Time.deltaTime;
            //         CharacterTransform.Rotate(Vector3.up, -rotationAmount);
            //         TargetAngle = Vector3.Angle(MuzzleDirection, TargetDirection);
            //
            //     }
            // }
            //
            Quaternion targetRotation;

            if (Vector3.Cross(CharacterDirection, TargetDirection).y >= 0)
            {
                Debug.Log("OnRight");
                targetRotation = Quaternion.LookRotation(RayCastDebugChild.transform.position - CharacterTransform.position);
                targetRotation.x = 0f;
                targetRotation.z = 0f;
                rotationSpeed = 2f;

            }
            else
            {
                targetRotation = Quaternion.LookRotation(TargetDirection);
                targetRotation.x = 0f;
                targetRotation.z = 0f;
                rotationSpeed = 0.5f;
            }
          
           Quaternion newRotation = Quaternion.Lerp(CharacterTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
          
           CharacterTransform.rotation = newRotation;
        }
    }
    void SetIkWeightON()
    {
        if(!IK_on)
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration2));

            aimIK.Weight = Mathf.Lerp(0f, 0.8f, t);

            if (t >= 1f)
            {
                interpolationTimer = 0f;
                IK_on = true;
            }

        }
        
    }

    void SetIkWeightOFF()
    {
        if(IK_on)
        {
            interpolationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(interpolationTimer / (interpolationDuration));

            aimIK.Weight = Mathf.Lerp(aimIK.Weight, 0f, t);

            if (t >= 1f)
            {
                interpolationTimer = 0f;
                IK_on = false;
            }
            
        }
       
    }

    void SetEngageBurnOut()
    {
        burnedOutEngaged = true;
    }

    public void EnableScript()
    {
        enabled = true;
    }

    public void DisableScript()
    {
        enabled = false;
    }
}
