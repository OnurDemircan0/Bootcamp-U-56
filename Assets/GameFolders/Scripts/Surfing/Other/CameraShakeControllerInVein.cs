using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraShakeControllerInVein : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCameraAim;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCameraFollow;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCameraVirusShow;

    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinAim;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinFollow;
    private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlinVirusShow;

    void Awake()
    {
        //cinemachineVirtualCamera = gameObject.GetComponent<CinemachineVirtualCamera>();

        try
        {
            cinemachineBasicMultiChannelPerlinAim = cinemachineVirtualCameraAim.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        catch(Exception e)
        {
            cinemachineBasicMultiChannelPerlinAim = null;
        }

        try
        {
            cinemachineBasicMultiChannelPerlinFollow = cinemachineVirtualCameraFollow.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        catch (Exception e)
        {
            cinemachineBasicMultiChannelPerlinFollow = null;
        }


        try
        {
            cinemachineBasicMultiChannelPerlinVirusShow = cinemachineVirtualCameraVirusShow.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        catch (Exception e)
        {
            cinemachineBasicMultiChannelPerlinVirusShow = null;
        }

    }

    private void Start()
    {
        //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 5.0f;

        //StartCoroutine(cameraShakeIEnumerator(1.5f, 3.5f,0.25f));

        //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1.25f;
    }

    public void cameraShake(float intensity, float fullIntensityTime, float goToZeroTime)
    {
        StartCoroutine(cameraShakeIEnumerator(intensity, fullIntensityTime, goToZeroTime));
    }


    private void Update()
    {
        if(cinemachineBasicMultiChannelPerlinAim == null)
        {
            try
            {
                cinemachineBasicMultiChannelPerlinAim = cinemachineVirtualCameraAim.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            catch (Exception e)
            {
                cinemachineBasicMultiChannelPerlinAim = null;
            }
        }

        if (cinemachineBasicMultiChannelPerlinFollow == null)
        {
            try
            {
                cinemachineBasicMultiChannelPerlinFollow = cinemachineVirtualCameraFollow.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            catch (Exception e)
            {
                cinemachineBasicMultiChannelPerlinFollow = null;
            }
        }

        if (cinemachineBasicMultiChannelPerlinVirusShow == null)
        {
            try
            {
                cinemachineBasicMultiChannelPerlinVirusShow = cinemachineVirtualCameraVirusShow.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
            catch (Exception e)
            {
                cinemachineBasicMultiChannelPerlinVirusShow = null;
            }
        }
    }



    IEnumerator cameraShakeIEnumerator(float intensity, float fullIntensityTime, float goToZeroTime)
    {
        /*
        cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;
        */


        try
        {
            cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
        }
        catch(Exception e)
        {

        }

        try
        {
            cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
        }
        catch (Exception e)
        {

        }

        try
        {
            cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;
        }
        catch (Exception e)
        {

        }
        
        yield return new WaitForSeconds(fullIntensityTime);

        //cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;


        while (intensity > 0)
        {
            intensity -= intensity * 0.033f / goToZeroTime;

            /*
            cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
            cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;*/

            try
            {
                cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
            }
            catch (Exception e)
            {

            }

            try
            {
                cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
            }
            catch (Exception e)
            {

            }

            try
            {
                cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;
            }
            catch (Exception e)
            {

            }

            yield return new WaitForSeconds(0.033f); //Saniyenin 30 da biri kadar bekle




            /*
            intensity -= intensity * (goToZeroTime / 30.0f);

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

            yield return new WaitForSeconds(1.0f / 30.0f); //Saniyenin 30 da biri kadar bekle
            */

            if (intensity < 0.3f)
            {
                intensity = 0;

                /*
                cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
                cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
                cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;
                */

                try
                {
                    cinemachineBasicMultiChannelPerlinAim.m_AmplitudeGain = intensity;
                }
                catch (Exception e)
                {

                }

                try
                {
                    cinemachineBasicMultiChannelPerlinFollow.m_AmplitudeGain = intensity;
                }
                catch (Exception e)
                {

                }

                try
                {
                    cinemachineBasicMultiChannelPerlinVirusShow.m_AmplitudeGain = intensity;
                }
                catch (Exception e)
                {

                }
            }

        }

        
        

        yield return null;
    }
}
