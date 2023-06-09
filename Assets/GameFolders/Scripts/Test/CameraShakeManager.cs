using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
    [SerializeField]
    StarterAssetsInputs starterAssetsInputs;

    [SerializeField]
    ThirdPersonShooterController thirdPersonShooterController;

    bool Shaked = false;
    public bool mimicExplode = false;

    [SerializeField]
    float ShakeIntesity;

    private void Update()
    {
        if (starterAssetsInputs != null)
        {
            if(starterAssetsInputs.shoot && !mimicExplode && !thirdPersonShooterController.burnedOut)
            {
                //Debug.Log("Inside shoot IF");
                CinemachineCameraShaker.Instance.SetIntensityShakeAimManuel(ShakeIntesity);

                Shaked = true;


            }
            else
            {
                if (Shaked && !starterAssetsInputs.shoot && !mimicExplode)
                {
                   //Debug.Log("In Restart Shake");
                    CinemachineCameraShaker.Instance.SetIntensityShakeAimManuel(0f);
               
                    Shaked = false;
                }
                else
                {
                    return;
                }
            }
        }
    }
}
