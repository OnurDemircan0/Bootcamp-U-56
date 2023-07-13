using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfTheLevelHeart : MonoBehaviour
{
    [SerializeField]
    float speedMultiplier = 2.5f;

    [SerializeField]
    StarterAssetsInputs starterAssetsInputs;

    [SerializeField]
    ThirdPersonController personController;

    [SerializeField]
    Animator MedBOTanimator;


    [SerializeField]
    FadeCamera fadeCamera;

    bool triggerEntered = false;

    private void OnTriggerEnter(Collider other)
    {
        triggerEntered = true;
    }

    private void Update()
    {
        if(triggerEntered)
        {
            stopAllMovement();

            Invoke("FadeEffectON", 1.2f);
        }
    }

    void stopAllMovement()
    {
        MedBOTanimator.SetFloat("Speed", 6f);

        personController.MoveSpeed *= speedMultiplier;

        starterAssetsInputs.sprint = true;
        starterAssetsInputs.move.y = 1f;
        starterAssetsInputs.move.x = 0f;
    }

    void FadeEffectON()
    {
        fadeCamera.isDead = true;
    }
}
