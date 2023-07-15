using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitFeelingController : MonoBehaviour
{
    [SerializeField] RawImage crossHairImage;
    private RectTransform crossHairRectTransform;

    private MedicineSwitching medicineSwitching;

    private CameraShakeControllerInVein cameraShakeControllerInVein;

    [SerializeField] private AudioSource surfingAlyuvarAudioSource;

    [SerializeField] private AudioClip killExplosionSound;
    [SerializeField] private AudioClip mutateSound;



    [SerializeField] private float changeSizeSpeedForHit;
    [SerializeField] private float changeSizeSpeedForKill;
    [SerializeField] private float changeSizeSpeedForMutate;
    [SerializeField] private float changeRotationSpeedForMutate;

    [SerializeField] private float minChangeSizeValueForHit;
    [SerializeField] private float maxChangeSizeValueForHit;

    [SerializeField] private float minChangeSizeValueForKill;
    [SerializeField] private float maxChangeSizeValueForKill;

    [SerializeField] private float minChangeSizeValueForMutate;
    [SerializeField] private float maxChangeSizeValueForMutate;

    [SerializeField] private float minChangeRotationValueForMutate;
    [SerializeField] private float maxChangeRotationValueForMutate;

    [SerializeField] private float waitTimeAfterReachMaxSizeAndRotation;

    [SerializeField] private float cameraShakeIntensityForKill;
    [SerializeField] private float cameraShakefullIntensityTimeForKill;
    [SerializeField] private float cameraShakeGoToZeroTimeForKill;

    [SerializeField] private float cameraShakeIntensityForMutate;
    [SerializeField] private float cameraShakefullIntensityTimeForMutate;
    [SerializeField] private float cameraShakeGoToZeroTimeForMutate;

    [SerializeField] private Image dnaImage;

    [SerializeField] private float minColorAlpha;
    [SerializeField] private float maxColorAlpha;

    [SerializeField] private float changeColorAlphaSpeed;

    private bool changeColorToZeroCompleteControl;
    private bool changeColorToOneCompleteControl;

    private float nowSizeCrossHair;

    private bool killedControl;

    private bool changeSizeCompleteControl;
    private bool changeSizeToMinCompleteControl;
    private bool changeSizeToMaxCompleteControl;

    private bool changeRotationToMinCompleteControl;
    private bool changeRotationToMaxCompleteControl;

    private bool waitBeforeChangeSizeControl;
    private bool waitBeforeChangeRotationControl;

    public bool mutationEfectFinishedControl;

    private void Awake()
    {
        crossHairRectTransform = crossHairImage.gameObject.GetComponent<RectTransform>();

        cameraShakeControllerInVein = gameObject.GetComponent<CameraShakeControllerInVein>();

        nowSizeCrossHair = crossHairRectTransform.sizeDelta.x;

        killedControl = false;

        changeSizeCompleteControl = true;

        mutationEfectFinishedControl = true;

        medicineSwitching = gameObject.GetComponent<MedicineSwitching>();

        //StartCoroutine(cameraShakeControllerInVein.cameraShake(1.5f, 0.5f, 0.25f));


        //crossHairRectTransform.Rotate(new Vector3(0, 0, 45));
    }



    public void hittedVirus()
    {
        // Debug.Log("Vir�s Vuruldu");


        if (changeSizeCompleteControl == true && killedControl == false)
        {
            StartCoroutine(changeScaleCrossHairImageForHit());
        }
        
    }

    public void killedVirus()
    {
        //Debug.Log("Vir�s �ld�r�ld�");

        killedControl = true;

        cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForKill, cameraShakefullIntensityTimeForKill, cameraShakeGoToZeroTimeForKill);

        StartCoroutine(changeScaleCrossHairImageForKill());

        surfingAlyuvarAudioSource.PlayOneShot(killExplosionSound);

    }



    public void wrongMedicine(MutationControl mutationControl)
    {
        //Debug.Log("Mutasyon ge�ir");

        if(mutationEfectFinishedControl == true)
        {
            StartCoroutine(changeScaleAndRotationCrossHairImageForMutate());

            if(dnaImage != null)
            {
                StartCoroutine(changeColorAlphaDNAImage());
            }
            

            cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForMutate, cameraShakefullIntensityTimeForMutate, cameraShakeGoToZeroTimeForMutate);

            try
            {
                mutationControl.mutateVirus();
            }
            catch(Exception e)
            {

            }


            surfingAlyuvarAudioSource.PlayOneShot(mutateSound);
        }
        

        //Debug.Log("Mutasyon ge�irdi");
    }

    IEnumerator changeColorAlphaDNAImage()
    {
        //yield return null;

        changeColorToZeroCompleteControl = false;
        changeColorToOneCompleteControl = false;



        while (true)
        {

            if (changeColorToOneCompleteControl == false)
            {
                dnaImage.color += new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp at�� b�y�yor: ");
                //Debug.Log("Kalp at�� b�y�yor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a >= maxColorAlpha)
                {
                    changeColorToOneCompleteControl = true;

                    yield return new WaitForEndOfFrame();
                }
            }
            else if (changeColorToZeroCompleteControl == false)
            {
                dnaImage.color -= new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp at�� k���l�yor: ");
                //Debug.Log("Kalp at�� k���l�yor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a <= minColorAlpha)
                {
                    dnaImage.color = new Color(dnaImage.color.r, dnaImage.color.g, dnaImage.color.b, 0);

                    break;
                }

            }

            yield return new WaitForEndOfFrame();

        }

    }


    IEnumerator changeScaleCrossHairImageForHit()
    {

        changeSizeCompleteControl = false;

        changeSizeToMinCompleteControl = false;
        changeSizeToMaxCompleteControl = false;

        //Debug.Log("crossHair boyut de�i�meye ba�lad�");

        //crossHairRectTransform.sizeDelta = new Vector2(300, 300);

        while (true)
        {
            yield return null;


            //Debug.Log("crossHair boyutu �al���yor");

            if (changeSizeToMaxCompleteControl == false)
            {
                //Debug.Log("crossHair boyutu b�y�yor");

                crossHairRectTransform.sizeDelta += new Vector2(0.1f, 0.1f) * changeSizeSpeedForHit * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x >= maxChangeSizeValueForHit)
                {
                    //Debug.Log("crossHair Max Boyuta Ula��ld�");
                    changeSizeToMaxCompleteControl = true;
                }
            }
            else if (changeSizeToMinCompleteControl == false)
            {
                //Debug.Log("crossHair boyutu k���l�yor");

                crossHairRectTransform.sizeDelta -= new Vector2(0.1f, 0.1f) * changeSizeSpeedForHit * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x <= minChangeSizeValueForHit)
                {
                    //Debug.Log("crossHair Min Boyuta Ula��ld�");
                    changeSizeToMinCompleteControl = true;
                }
            }

            if(changeSizeToMinCompleteControl == true && changeSizeToMinCompleteControl == true)
            {
                //Debug.Log("crossHair boyut de�i�imi durdu");
                changeSizeCompleteControl = true;
                break;
            }

            if(killedControl == true)
            {
                break;
            }
        }

    }

    IEnumerator changeScaleCrossHairImageForKill()
    {
        crossHairImage.color = Color.red;

        changeSizeCompleteControl = false;

        changeSizeToMinCompleteControl = false;
        changeSizeToMaxCompleteControl = false;

        //Debug.Log("crossHair boyut de�i�meye ba�lad� �ld�rmeye g�re");

        //crossHairRectTransform.sizeDelta = new Vector2(300, 300);

        while (true)
        {
            yield return null;

            if (changeSizeToMaxCompleteControl == false)
            {
                //Debug.Log("crossHair boyutu b�y�yor �ld�rmeye g�re");

                crossHairRectTransform.sizeDelta += new Vector2(0.1f, 0.1f) * changeSizeSpeedForKill * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x >= maxChangeSizeValueForKill)
                {
                    //Debug.Log("crossHair Max Boyuta Ula��ld� �ld�rmeye g�re");
                    changeSizeToMaxCompleteControl = true;
                }
            }
            else if (changeSizeToMinCompleteControl == false)
            {
                //Debug.Log("crossHair boyutu k���l�yor �ld�rmeye g�re");

                crossHairRectTransform.sizeDelta -= new Vector2(0.1f, 0.1f) * changeSizeSpeedForKill * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x <= minChangeSizeValueForKill)
                {
                    //Debug.Log("crossHair Min Boyuta Ula��ld� �ld�rmeye g�re");
                    changeSizeToMinCompleteControl = true;
                }
            }

            if (changeSizeToMinCompleteControl == true && changeSizeToMinCompleteControl == true)
            {
                //Debug.Log("crossHair boyut de�i�imi durdu �ld�rmeye g�re");
                changeSizeCompleteControl = true;
                killedControl = false;

                medicineSwitching.SelectWeapon();
                break;
            }
        }

    }


    IEnumerator changeScaleAndRotationCrossHairImageForMutate()
    {
        mutationEfectFinishedControl = false;

        crossHairImage.color = Color.black;

        changeSizeCompleteControl = false;

        changeSizeToMinCompleteControl = false;
        changeSizeToMaxCompleteControl = false;

        changeRotationToMinCompleteControl = false;
        changeRotationToMaxCompleteControl = false;

        waitBeforeChangeSizeControl = false;
        waitBeforeChangeRotationControl = false;

        //Debug.Log("crossHair boyut de�i�meye ba�lad� Mutasyona g�re");

        

        while (true)
        {
            yield return null;

            //Debug.Log("crossHairRectTransform.rotation.eulerAngles.z: " + crossHairRectTransform.rotation.eulerAngles.z);

            if (changeSizeToMaxCompleteControl == false)
            {
                //Debug.Log("crossHair boyutu b�y�yor Mutasyona g�re");

                crossHairRectTransform.sizeDelta += new Vector2(0.1f, 0.1f) * changeSizeSpeedForMutate * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x >= maxChangeSizeValueForMutate)
                {
                    //Debug.Log("crossHair Max Boyuta Ula��ld� Mutasyona g�re");
                    changeSizeToMaxCompleteControl = true;
                }
            }
            else if (changeSizeToMinCompleteControl == false && waitBeforeChangeSizeControl == true)
            {
                //Debug.Log("crossHair boyutu k���l�yor Mutasyona g�re");

                crossHairRectTransform.sizeDelta -= new Vector2(0.1f, 0.1f) * changeSizeSpeedForMutate * Time.deltaTime;

                if (crossHairRectTransform.sizeDelta.x <= minChangeSizeValueForMutate)
                {
                    //Debug.Log("crossHair Min Boyuta Ula��ld� Mutasyona g�re");
                    changeSizeToMinCompleteControl = true;
                }
            }


            if (changeRotationToMaxCompleteControl == false)
            {
                //Debug.Log("crossHair d�n�yor Mutasyona g�re");


                crossHairRectTransform.Rotate(new Vector3(0, 0, 0.1f) * changeRotationSpeedForMutate * Time.deltaTime);

                if (crossHairRectTransform.rotation.eulerAngles.z >= maxChangeRotationValueForMutate)
                {
                    //Debug.Log("crossHair Max D�nme Noktas�na Ula��ld� Mutasyona g�re");
                    changeRotationToMaxCompleteControl = true;
                }
            }
            else if (changeRotationToMinCompleteControl == false && waitBeforeChangeRotationControl == true)
            {
                //Debug.Log("crossHair boyutu normal haline d�n�yor Mutasyona g�re");

                crossHairRectTransform.Rotate(new Vector3(0, 0, -0.1f) * changeRotationSpeedForMutate * Time.deltaTime);

                if (crossHairRectTransform.rotation.eulerAngles.z <= minChangeRotationValueForMutate)
                {
                    crossHairRectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    //Debug.Log("crossHair Min D�nme Noktas�na Ula��ld� Mutasyona g�re");
                    changeRotationToMinCompleteControl = true;
                }
            }

            if (changeSizeToMinCompleteControl == true && changeSizeToMinCompleteControl == true 
                && changeRotationToMinCompleteControl == true && changeRotationToMaxCompleteControl == true)
            {
                //Debug.Log("crossHair boyut de�i�imi durdu Mutasyona g�re");
                changeSizeCompleteControl = true;
                killedControl = false;

                medicineSwitching.SelectWeapon();

                mutationEfectFinishedControl = true;

                break;
            }
            else if (changeSizeToMaxCompleteControl == true && waitBeforeChangeSizeControl == false 
                && changeRotationToMaxCompleteControl == true && waitBeforeChangeRotationControl == false)
            {
                //Debug.Log("crossHair boyut full rotasyon full ula�t� Mutasyona g�re");

                yield return new WaitForSeconds(waitTimeAfterReachMaxSizeAndRotation);


                waitBeforeChangeSizeControl = true;
                waitBeforeChangeRotationControl = true;

            }
        }

    }
}
