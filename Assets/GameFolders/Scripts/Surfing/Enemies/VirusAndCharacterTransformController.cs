using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusAndCharacterTransformController : MonoBehaviour
{
    VirusController virusController;

    SurfingControllerV2 surfingControllerV2;

    private Image borderImage;

    [SerializeField] private AudioClip damageToPlayerSound;

    private HeartIconShowController heartIconShowController;

    [SerializeField] private float maxFarCharecterForDamage;

    [SerializeField] private float damageCountForMultiplySize;

    private AudioSource surfingAlyuvarAudioSource;

    private CameraShakeControllerInVein cameraShakeControllerInVein;

    private bool beforeDamaged;


    [SerializeField] private float minColorAlpha;
    [SerializeField] private float maxColorAlpha;

    [SerializeField] private float changeColorAlphaSpeed;

    [SerializeField] private float cameraShakeIntensityForGetDamage;
    [SerializeField] private float cameraShakefullIntensityTimeForGetDamage;
    [SerializeField] private float cameraShakeGoToZeroTimeForGetDamage;

    [SerializeField] private float destroyWaitTimeAfterPassCharacter;


    private bool changeColorToZeroCompleteControl;
    private bool changeColorToOneCompleteControl;


    void Awake()
    {
        virusController = gameObject.GetComponent<VirusController>();

        surfingControllerV2 = GameObject.Find("Surf Alyuvar 2").GetComponent<SurfingControllerV2>();
        surfingAlyuvarAudioSource = surfingControllerV2.gameObject.GetComponent<AudioSource>();

        borderImage = GameObject.Find("Image Border").gameObject.GetComponent<Image>();

        heartIconShowController = GameObject.Find("GameManager").gameObject.GetComponent<HeartIconShowController>();

        cameraShakeControllerInVein = GameObject.Find("GameManager").gameObject.GetComponent<CameraShakeControllerInVein>();

        beforeDamaged = false;



    }


    void Update()
    {
        if(beforeDamaged == false)
        {
            if (surfingControllerV2.dstTravelled >= virusController.dstTravelled + maxFarCharecterForDamage && gameObject.transform.localScale.x > 0.1f)
            {
                Debug.Log("Karekter Hasar Aldý");

                surfingAlyuvarAudioSource.PlayOneShot(damageToPlayerSound);

                borderImage.color = new Color(borderImage.color.r, borderImage.color.g, borderImage.color.b, 0);
                StartCoroutine(changeColorAlphaBorderImage());

                heartIconShowController.decreaseHealth(gameObject.transform.localScale.x * damageCountForMultiplySize);

                cameraShakeControllerInVein.cameraShake(cameraShakeIntensityForGetDamage, cameraShakefullIntensityTimeForGetDamage, cameraShakeGoToZeroTimeForGetDamage);

                //gameObject.transform.localScale = new Vector3(0, 0, 0);

                beforeDamaged = true;

                Destroy(gameObject, destroyWaitTimeAfterPassCharacter);
            }
            
        }
    }


    IEnumerator changeColorAlphaBorderImage()
    {
        //yield return null;

        changeColorToZeroCompleteControl = false;
        changeColorToOneCompleteControl = false;



        while (true)
        {

            if (changeColorToOneCompleteControl == false)
            {
                borderImage.color += new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp atýþ büyüyor: ");
                //Debug.Log("Kalp atýþ büyüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (borderImage.color.a >= maxColorAlpha)
                {
                    changeColorToOneCompleteControl = true;

                    yield return new WaitForEndOfFrame();
                }
            }
            else if (changeColorToZeroCompleteControl == false)
            {
                borderImage.color -= new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp atýþ küçülüyor: ");
                //Debug.Log("Kalp atýþ küçülüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (borderImage.color.a <= minColorAlpha)
                {
                    borderImage.color = new Color(borderImage.color.r, borderImage.color.g, borderImage.color.b, 0);

                    break;
                }

            }

            yield return new WaitForEndOfFrame();

        }

    }
}
