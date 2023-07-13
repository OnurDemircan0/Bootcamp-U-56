using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitBeforeReady : MonoBehaviour
{
    [SerializeField]
    float timeBeforeReady = 5f;

    [SerializeField]
    ParticleSystem readyParticle;

    [SerializeField]
    AudioSource Explosion;

    [SerializeField]
    float cameraShakeIntensity = 5f;

    [SerializeField]
    float cameraShakeTime = 1f;

    [SerializeField]
    CameraShakeManager shakeManager;
    

    public bool triggeed = false;
    public bool isReady = true;

    bool AudioPlayed = false;
    bool timeStarted = false;
    bool shakeTrigger = false;

    private float duration;



    private void Update()
    {
        if(isReady)
        {
            AudioPlayed = false;
            readyParticle.gameObject.SetActive(true);
        }

        if(triggeed)
        {
            readyParticle.Stop() ;
            isReady = false;


            if(!shakeTrigger)
            {
                shakeManager.mimicExplode = true;
                Invoke("ResetShakeManager", 1.5f);

                CinemachineCameraShaker.Instance.ShakeCameraAim(cameraShakeIntensity, cameraShakeTime, true);
                CinemachineCameraShaker.Instance.ShakeCameraFollow(cameraShakeIntensity, cameraShakeTime, true);

                shakeTrigger = true;
            }

            if (!AudioPlayed)
            {
                Explosion.Play();
                AudioPlayed = true;
            }

            if(!timeStarted) 
            {
                duration = timeBeforeReady;
                timeStarted = true;
            }
            
            duration -= Time.deltaTime;

            if(duration <= 0 ) 
            {
                readyParticle.gameObject.SetActive(false);

                isReady = true;
                triggeed = false;
                timeStarted = false;
                shakeTrigger = false;
                Debug.Log("Explosion is ready");
            }


        }
    }

    void ResetShakeManager()
    {
        shakeManager.mimicExplode = false;
    }
}
