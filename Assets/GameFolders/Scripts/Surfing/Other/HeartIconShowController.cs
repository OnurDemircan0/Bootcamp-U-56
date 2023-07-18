using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HeartIconShowController : MonoBehaviour
{
    [SerializeField] private RectTransform heartIconImageRectTransform;

    [SerializeField] private Death death;

    private Image heartIconImage;

    [SerializeField] private float waitTimeForReloadThisScene;

    [SerializeField] private TMP_Text healthCountText;

    private HeartPulseShowController heartPulseShowController;

    [SerializeField] private AudioSource surfingAlyuvarAudioSource;

    [SerializeField] private AudioClip heartPulseDanger;
    [SerializeField] private AudioClip heartPulseStop;

    [SerializeField] private float maxHealthForDanger;

    public int nowHealth; // �lk Nab�z // Bu ka� ise kalp dakika o kadar say�da b�y�y�p k���lecek

    [SerializeField] private float changeSizeSpeedForPulse; // Nab�z say�s�na g�re belirle

    [SerializeField] private float minChangeSizeValue;
    [SerializeField] private float maxChangeSizeValue;

    private float heartPulseAndWaitTime; // Kalbin at�p ve att�ktan sonra dinlenme s�resinin toplam ne kadar olmas� gerekiyor ki dakikada nab�z say�s� kadar ats�n
    private float waitTimeEachPulse;

    [SerializeField] private float maxWaitTimeEachPulse; // Nab�z 40 alt�nda kalp hi� atm�yormu� gibi oluyor bunu engellemek i�in maksimum bekleme s�resi var

    private float averageHeartPulseTime;

    private bool averageHeartPulseTimeControl;


    private bool changeSizeToMinCompleteControl;
    private bool changeSizeToMaxCompleteControl;

    private void Start()
    {

    }

    private void Update()
    {
        if (nowHealth < 0)
        {
            nowHealth = 0;

            heartPulseShowController.deadControl = true;

            if (death != null)
            {
                death.health = -1;

                Invoke("reloadThisScene", waitTimeForReloadThisScene);
            }
        }


        if (nowHealth <= 0)
        {
            nowHealth = 0;
            healthCountText.SetText(nowHealth.ToString());

            surfingAlyuvarAudioSource.clip = heartPulseStop;
            surfingAlyuvarAudioSource.loop = true;
            surfingAlyuvarAudioSource.Play();

            heartIconImage.color = new Color(0, 0, 0);
        }
        else if (nowHealth <= maxHealthForDanger)
        {
            nowHealth = 0;
            healthCountText.SetText(nowHealth.ToString());

            surfingAlyuvarAudioSource.clip = heartPulseDanger;
            surfingAlyuvarAudioSource.loop = true;
            surfingAlyuvarAudioSource.Play();
        }

        healthCountText.SetText(nowHealth.ToString());
    }

    private void reloadThisScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    public void decreaseHealth(float decreaseCount)
    {
        nowHealth -= (int) decreaseCount ;

        if(nowHealth < 0)
        {
            nowHealth = 0;

            heartPulseShowController.deadControl = true;

            if(death != null)
            {
                death.health = -1;

                Invoke("reloadThisScene", waitTimeForReloadThisScene);
            }
        }


        if(nowHealth <= 0)
        {
            surfingAlyuvarAudioSource.clip = heartPulseStop;
            surfingAlyuvarAudioSource.loop = true;
            surfingAlyuvarAudioSource.Play();

            heartIconImage.color = new Color(0, 0, 0);
        }
        else if(nowHealth <= maxHealthForDanger)
        {
            surfingAlyuvarAudioSource.clip = heartPulseDanger;
            surfingAlyuvarAudioSource.loop = true;
            surfingAlyuvarAudioSource.Play();
        }

        healthCountText.SetText(nowHealth.ToString());
    }

    public void setHealth(int health)
    {
        nowHealth = health;

        surfingAlyuvarAudioSource.Stop();

        healthCountText.SetText(nowHealth.ToString());


    }


    void Awake()
    {
        heartPulseShowController = gameObject.GetComponent<HeartPulseShowController>();

        averageHeartPulseTimeControl = false;

        heartIconImage = heartIconImageRectTransform.gameObject.GetComponent<Image>();

        //heartIconImageRectTransform.sizeDelta += new Vector2(10, 10);

        StartCoroutine(changeScaleHeartImage());
    }



    IEnumerator changeScaleHeartImage()
    {
        //yield return null;

        changeSizeToMinCompleteControl = false;
        changeSizeToMaxCompleteControl = false;

        
        float firstTime;
        float lastTime;

        firstTime = Time.time;

        waitTimeEachPulse = 0;
        

        while (true)
        {
            if(nowHealth <= 0)
            {
                break;
            }

            
            heartPulseAndWaitTime =  60.0f / nowHealth;

            if (averageHeartPulseTimeControl == false)
            {
                firstTime = Time.time;

                //Debug.Log("Kalp at�� firstTime: " + firstTime.ToString());

                averageHeartPulseTimeControl = true;
            }
            


            if (changeSizeToMaxCompleteControl == false)
            {
                heartIconImageRectTransform.sizeDelta += new Vector2(0.1f, 0.1f) * changeSizeSpeedForPulse * Time.deltaTime;

                //Debug.Log("Kalp at�� b�y�yor: ");
                //Debug.Log("Kalp at�� b�y�yor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (heartIconImageRectTransform.sizeDelta.x >= maxChangeSizeValue)
                {
                    changeSizeToMaxCompleteControl = true;
                    changeSizeToMinCompleteControl = false;

                    yield return new WaitForEndOfFrame();
                }
            }
            else if (changeSizeToMinCompleteControl == false)
            {
                heartIconImageRectTransform.sizeDelta -= new Vector2(0.1f, 0.1f) * changeSizeSpeedForPulse * Time.deltaTime;

                //Debug.Log("Kalp at�� k���l�yor: ");
                //Debug.Log("Kalp at�� k���l�yor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (heartIconImageRectTransform.sizeDelta.x <= minChangeSizeValue)
                {
                    changeSizeToMinCompleteControl = true;
                    changeSizeToMaxCompleteControl = false;


                    
                    lastTime = Time.time;

                    averageHeartPulseTimeControl = false;

                    averageHeartPulseTime = lastTime - firstTime;

                    //Debug.Log("Kalp at�� lastTime: " + lastTime.ToString());

                    //Debug.Log("Kalp at�� saniyesi: " + averageHeartPulseTime.ToString());

                    waitTimeEachPulse = heartPulseAndWaitTime - averageHeartPulseTime;

                    if(waitTimeEachPulse > maxWaitTimeEachPulse)
                    {
                        waitTimeEachPulse = maxWaitTimeEachPulse;
                    }

                    //Debug.Log("Kalp at�� dinlenme saniyesi: " + (waitTimeEachPulse).ToString());

                    yield return new WaitForSeconds(waitTimeEachPulse);


                    yield return new WaitForEndOfFrame();
                }

                
            }

            yield return new WaitForEndOfFrame();


        }

    }
}
