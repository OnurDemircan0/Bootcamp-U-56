using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPulseShowController : MonoBehaviour
{
    [SerializeField] private RectTransform heartPulseImageRectTransform;

    [SerializeField] private float pulseShowSpeed;

    [SerializeField] private float firstPositionX;
    [SerializeField] private float lastPositionX;

    [SerializeField] private float deadFirstPositionX;

    [SerializeField] private float onePulseSizeX;

    public bool deadControl;

    private bool deadPositionReachedControl;

    



    void Start()
    {
        deadControl = false;
        deadPositionReachedControl = false;

        heartPulseImageRectTransform.anchoredPosition = new Vector2(firstPositionX, 0);
    }


    void Update()
    {
        if(deadControl == false)
        {
            heartPulseImageRectTransform.anchoredPosition += new Vector2(1, 0) * Time.deltaTime * pulseShowSpeed * -1;

            if(heartPulseImageRectTransform.anchoredPosition.x <= lastPositionX)
            {
                heartPulseImageRectTransform.anchoredPosition = new Vector2(firstPositionX, 0);
            }
        }
        else if(deadPositionReachedControl == false)
        {
            /*
            if (heartPulseImageRectTransform.anchoredPosition.x > lastPositionX)
            {
                // Eðer Ölürse bir daha nabýz atýþý göstermemek için nabýz resminin hemen düz çizgiden öncesine gitmek için
                heartPulseImageRectTransform.anchoredPosition = new Vector2(heartPulseImageRectTransform.anchoredPosition.x + onePulseSizeX, 0);
            }
            */



            heartPulseImageRectTransform.anchoredPosition += new Vector2(1, 0) * Time.deltaTime * pulseShowSpeed * -1;

            if (heartPulseImageRectTransform.anchoredPosition.x <= deadFirstPositionX)
            {
                heartPulseImageRectTransform.anchoredPosition = new Vector2(deadFirstPositionX, 0);

                deadPositionReachedControl = true;
            }
        }
    }
}
