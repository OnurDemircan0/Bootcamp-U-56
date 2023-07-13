using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DnaImageShow : MonoBehaviour
{

    private bool changeColorToZeroCompleteControl;
    private bool changeColorToOneCompleteControl;


    void Start()
    {
        
    }


    void Update()
    {
        
    }

    /*
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

                //Debug.Log("Kalp atýþ büyüyor: ");
                //Debug.Log("Kalp atýþ büyüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a >= maxColorAlpha)
                {
                    changeColorToOneCompleteControl = true;

                    yield return new WaitForEndOfFrame();
                }
            }
            else if (changeColorToZeroCompleteControl == false)
            {
                dnaImage.color -= new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp atýþ küçülüyor: ");
                //Debug.Log("Kalp atýþ küçülüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a <= minColorAlpha)
                {
                    dnaImage.color = new Color(dnaImage.color.r, dnaImage.color.g, dnaImage.color.b, 0);

                    break;
                }

            }

            yield return new WaitForEndOfFrame();

        }

    }
    */
}
