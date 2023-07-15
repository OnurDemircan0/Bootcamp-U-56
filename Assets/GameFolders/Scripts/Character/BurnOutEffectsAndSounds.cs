using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class BurnOutEffectsAndSounds : MonoBehaviour
{
    [SerializeField]
    AudioSource burnOutSound;

    [SerializeField]
    GameObject burnOutEffect;

    ThirdPersonShooterController thirdPersonShooterController;

    bool triggered = false;

    private void Awake()
    {
        thirdPersonShooterController = GetComponent<ThirdPersonShooterController>();
    }

    private void Update()
    {
        if(thirdPersonShooterController != null ) 
        {
            if(thirdPersonShooterController.burnedOut && !triggered)
            {
                burnOutEffect.SetActive(true);
                burnOutSound.Play();
                triggered = true;
            }
            else if (thirdPersonShooterController.burnedOut == false && triggered)
            {
                Invoke("StopBurnOutEffect", 2f);
                triggered = false;
            }
        }
    }


    void StopBurnOutEffect()
    {
        burnOutEffect.SetActive(false);
    }
}
