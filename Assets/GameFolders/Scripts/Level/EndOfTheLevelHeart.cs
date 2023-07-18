using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    bool invoked;

    private void OnTriggerEnter(Collider other)
    {
        triggerEntered = true;
    }


    private void Update()
    {
        if(triggerEntered)
        {
            stopAllMovement();

            if(!invoked)
            {
                Invoke("FadeEffectON", 1.2f);
                invoked = true;
            }
            
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

        //goNextLevel();
        Invoke("goNextLevel", 2.5f);
    }

    private void goNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
