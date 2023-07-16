using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOutMeterSound : MonoBehaviour
{
    [SerializeField]
    AudioSource burnOutMeterSound;

    [SerializeField]
    ParticleSystem particleFire;

    [SerializeField]
    ParticleSystem particleSmoke;

    [SerializeField]
    AudioSource blankShotAudio;

    [SerializeField]
    AudioSource RechargeAudio;

    ThirdPersonShooterController thirdPersonShooterController;

    StarterAssetsInputs assetsInputs;

    Animator animator;

    float emissionDuration = 25f;
    float emissionTimer = 0f;

    bool insideTheFirst = false;
    bool insideTheSecond = false;

    bool played = false;
    bool stopped = false;

    bool playedBlankAudio;
    bool triggeredBurnOutOnce;

    private ParticleSystem.EmissionModule fireEmissionModule;
    private ParticleSystem.EmissionModule smokeEmissionModule;
    private void Awake()
    {
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        assetsInputs = GetComponent<StarterAssetsInputs>();

        animator = GetComponent<Animator>();

       // fireEmissionModule = particleFire.emission;
       // smokeEmissionModule = particleSmoke.emission;
    }

    private void Update()
    {
        if (thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit > 0.05f &&
    !(Mathf.Approximately(thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit, 1f) ||
      Mathf.Approximately(thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit, 0.98f)) && !played)
        {
            
                burnOutMeterSound.pitch = 1f;
                burnOutMeterSound.Play();
                played = true;

               // insideTheFirst = true;
               //
               // insideTheSecond = false;

        }
        else if (thirdPersonShooterController.burnedOut)
        {
            burnOutMeterSound.Stop();
            played = false;

            triggeredBurnOutOnce = true;


           // insideTheFirst = false;

           // fireEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
           // smokeEmissionModule.rateOverTime = new ParticleSystem.MinMaxCurve(0f);
        }

        else if (!assetsInputs.shoot && played && thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit > 0.05f)
        {
            float targetPitch = (thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit) * 2 - 1; // Scale the burnOutMeter value to range from -1 to 1
            float lerpSpeed = 1f / thirdPersonShooterController.burnOutTime;
            burnOutMeterSound.pitch = Mathf.Lerp(-1f, targetPitch, Time.deltaTime * lerpSpeed);

            //insideTheFirst = false;
            //
            //insideTheSecond = true;
        }

       else if( assetsInputs.shoot && played && thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit > 0.05f)
       {
           burnOutMeterSound.pitch = 1f;
       }

        else if (thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit < 0.05f)
        {
            burnOutMeterSound.Stop();
            played = false;




           // insideTheFirst = false;
        }



      if(thirdPersonShooterController.burnedOut && assetsInputs.shoot && !playedBlankAudio) 
      {
          blankShotAudio.Play();
          playedBlankAudio = true;

         // animator.SetBool("burnedOut", true);
         //   Invoke("ResetAnim", 0.250f);
      }
      else if (!assetsInputs.shoot && playedBlankAudio)
      {
          playedBlankAudio = false;
      }

      if(triggeredBurnOutOnce && !thirdPersonShooterController.burnedOut)
        {
            RechargeAudio.PlayOneShot(RechargeAudio.clip);
            triggeredBurnOutOnce = false;
        }
      

       //if (insideTheFirst)
       //{
       //    HigerEmission();
       //}
       //if(insideTheSecond)
       //{
       //    LowerEmission();
       //}
    }

    void ResetAnim()
    {
        animator.SetBool("burnedOut", false);
    }








   // void HigerEmission()
   // {
   //     emissionTimer += Time.deltaTime;
   //
   //     float normalizedTime = Mathf.Clamp01(emissionTimer / emissionDuration);
   //     float targetRate = Mathf.Lerp(0f, 5f, normalizedTime);
   //
   //     ParticleSystem.EmissionModule fireEmission = particleFire.emission;
   //     fireEmission.rateOverTime = targetRate;
   //
   //     ParticleSystem.EmissionModule smokeEmission = particleSmoke.emission;
   //     smokeEmission.rateOverTime = targetRate;
   //
   //     if (emissionTimer >= emissionDuration)
   //     {
   //         emissionTimer = 0f;
   //         insideTheFirst = false;
   //     }
   // }
   //
   // void LowerEmission()
   // {
   //     emissionTimer += Time.deltaTime;
   //
   //     float normalizedTime = Mathf.Clamp01(emissionTimer / emissionDuration);
   //     float targetRate = Mathf.Lerp(fireEmissionModule.rateOverTime.constant, 0f, normalizedTime);
   //
   //     ParticleSystem.EmissionModule fireEmission = particleFire.emission;
   //     fireEmission.rateOverTime = targetRate;
   //
   //     ParticleSystem.EmissionModule smokeEmission = particleSmoke.emission;
   //     smokeEmission.rateOverTime = targetRate;
   //
   //     if (emissionTimer >= emissionDuration)
   //     {
   //         emissionTimer = 0f;
   //         insideTheSecond = false;
   //     }
   // }
}