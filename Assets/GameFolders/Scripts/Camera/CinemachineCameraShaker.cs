using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CinemachineCameraShaker : MonoBehaviour
{
    public static CinemachineCameraShaker Instance { get; private set; }

    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualAimCamera;

    [SerializeField]
    CinemachineVirtualCamera cinemachineVirtualFollowCamera;

    float shakeTimerAim;
    float shakeTimerFollow;

    float shakeTimerAimSmooth;
    float shakeTimerFollowSmooth;

    float shakeTimerAimSmoothTotal;
    float shakeTimerFollowSmoothTotal;

    float shakeAimSmoothIntensity;
    float shakeFollowSmoothIntensity;

    float followOriginalIntensity;

    public NoiseSettings sixdShake;
    public NoiseSettings handHeld;

    private void Awake()
    {
        Instance = this;

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinFollow = cinemachineVirtualFollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        followOriginalIntensity = cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain;

       // Debug.Log("Follow Original Intensity is" + followOriginalIntensity);
    }


    public void ShakeCameraAim(float intensity, float time, bool isSmooth = false)
    {
        CinemachineBasicMultiChannelPerlin  cinemachineBasicMultiChannelPerlin = cinemachineVirtualAimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

        if (isSmooth ) { shakeAimSmoothIntensity = intensity; shakeTimerAimSmoothTotal = time; shakeTimerAimSmooth = time;  }
        else { shakeTimerAim = time; }
        
    }

    private void Update()
    {
        if (shakeTimerAim > 0)
        {
            shakeTimerAim -= Time.deltaTime;

            if (shakeTimerAim < 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualAimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
            }
        }

        if (shakeTimerFollow > 0)
        {
            shakeTimerFollow -= Time.deltaTime;

            if (shakeTimerFollow < 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualFollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_NoiseProfile = handHeld;
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = followOriginalIntensity;

            }
        }

        if (shakeTimerAimSmooth > 0)
        {
            //Debug.Log("In Shake Timer Aim Smooth");
            shakeTimerAimSmooth -= Time.deltaTime;

            if (shakeTimerAimSmooth < 0)
            {
                Debug.Log("Lerp Started");
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualAimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(shakeAimSmoothIntensity, 0f, 1 - (shakeTimerAimSmooth / shakeTimerAimSmoothTotal));
               // Debug.Log("Lerp Finished"); 
            }
        }

        if (shakeTimerFollowSmooth > 0)
        {
            shakeTimerFollowSmooth -= Time.deltaTime;

            if (shakeTimerFollowSmooth < 0)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualFollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(shakeFollowSmoothIntensity, 0f, 1-(shakeTimerFollowSmooth / shakeTimerFollowSmoothTotal));

                StartCoroutine(LerpAmplitudeAndSwitchProfile(cinemachineBasicMultiChannelPerlin));
            }
        }
    }


            public void SetIntensityShakeAimManuel(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualAimCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }




    public void ShakeCameraFollow(float intensity, float time, bool isSmooth = false)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualFollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = sixdShake;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        
        if (isSmooth) { shakeFollowSmoothIntensity = intensity; shakeTimerFollowSmoothTotal = time ; shakeTimerFollowSmooth = time ; }
        else { shakeTimerFollow = time; }
        
    }
    public void SetIntensityShakeFollowManuel(float intensity)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualFollowCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

    private IEnumerator LerpAmplitudeAndSwitchProfile(CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin)
    {
        float elapsedTime = 0f;
        float lerpTime = shakeTimerFollowSmoothTotal;

        float startAmplitude = cinemachineBasicMultiChannelPerlin.m_AmplitudeGain;
        float endAmplitude = 0f;

        while (elapsedTime < lerpTime)
        {
            float t = elapsedTime / lerpTime;
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(startAmplitude, endAmplitude, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = handHeld;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = followOriginalIntensity;
    }
}

