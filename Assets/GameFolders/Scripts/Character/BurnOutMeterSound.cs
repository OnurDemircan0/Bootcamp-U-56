using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnOutMeterSound : MonoBehaviour
{
    [SerializeField]
    AudioSource burnOutMeterSound;

    ThirdPersonShooterController thirdPersonShooterController;

    StarterAssetsInputs assetsInputs;

    bool played = false;

    private void Awake()
    {
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
        assetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit > 0.05f &&
    !(Mathf.Approximately(thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit, 1f) ||
      Mathf.Approximately(thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit, 0.98f)))
        {
            if(!played) 
            {
                burnOutMeterSound.pitch = 1f;
                burnOutMeterSound.Play();
                played = true;
            }
        }
        else if (thirdPersonShooterController.burnedOut)
        {
            burnOutMeterSound.Stop();
        }
        else if(!assetsInputs.shoot && played)
        {
            burnOutMeterSound.pitch = -1f;
        }
        else if(thirdPersonShooterController.burnOutMeter / thirdPersonShooterController.burnOutLimit < 0.05f)
        {
            burnOutMeterSound.Stop();
            played = false;
        }
    }
}
