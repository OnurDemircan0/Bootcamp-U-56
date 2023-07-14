using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DnaImageShow : MonoBehaviour
{
    [SerializeField] private Image dnaImage;

    [SerializeField] private float minColorAlpha;
    [SerializeField] private float maxColorAlpha;

    [SerializeField] private float changeColorAlphaSpeed;

    

    private bool hideDnaImageControl;
    private bool showDnaImageControl;

    private bool changeColorToZeroCompleteControl;
    private bool changeColorToOneCompleteControl;


    void Start()
    {
        showDnaImageControl = true;
        hideDnaImageControl = false;

        
        //showDna();
    }


    public void showDna()
    {
        if (showDnaImageControl)
        {
            StartCoroutine(changeColorAlphaDNAImage());

            showDnaImageControl = false;
        }
    }

    public void hideDna()
    {
        hideDnaImageControl = true;
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

                //Debug.Log("Kalp atýþ büyüyor: ");
                //Debug.Log("Kalp atýþ büyüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a >= maxColorAlpha)
                {
                    changeColorToOneCompleteControl = true;

                    yield return new WaitForEndOfFrame();
                }
            }
            else if (changeColorToZeroCompleteControl == false && hideDnaImageControl == true)
            {
                dnaImage.color -= new Color(0, 0, 0, 0.01f) * changeColorAlphaSpeed * Time.deltaTime;

                //Debug.Log("Kalp atýþ küçülüyor: ");
                //Debug.Log("Kalp atýþ küçülüyor heartIconImageRectTransform.sizeDelta.x: " + heartIconImageRectTransform.sizeDelta.x);

                if (dnaImage.color.a <= minColorAlpha)
                {
                    dnaImage.color = new Color(dnaImage.color.r, dnaImage.color.g, dnaImage.color.b, 0);


                    showDnaImageControl = true;
                    hideDnaImageControl = false;

                    break;
                }

            }

            yield return new WaitForEndOfFrame();

        }

    }
    
}
