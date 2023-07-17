using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupController : MonoBehaviour
{

    [SerializeField] private RectTransform panelRectTransform;
    [SerializeField] private TMP_Text didYouKnowThatSentencesText;

    [SerializeField] private Vector2 firstPositionOfPopupPanel;
    [SerializeField] private Vector2 targetPositionOfPopupPanel;
    

    [SerializeField] private float popupShowSpeed;

    [SerializeField] private float minDistanceForMoveDirectTargetPosition;


    private DolarApi dolarApi;

    //MEDBOT nanorobotunu maliyeti \n1 Milyon Dolar(26.10 Milyon TL)
    private string didYouKnowThatSentencesFirstPart = "MEDBOT nanorobotunu maliyeti \n1 Milyon Dolar(";
    private string didYouKnowThatSentencesSecondPart = " Milyon TL)";

    private void Awake()
    {
        dolarApi = gameObject.GetComponent<DolarApi>();

        panelRectTransform.localPosition = firstPositionOfPopupPanel;

        StartCoroutine(movePanel());


        StartCoroutine(getDolarInfo());

    }

    IEnumerator getDolarInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            editDolarText();
        }
    }

    private void editDolarText()
    {
        didYouKnowThatSentencesText.SetText(didYouKnowThatSentencesFirstPart + dolarApi.getUsdToTlInfo() + didYouKnowThatSentencesSecondPart);
    }


    public void showPanel()
    {
        StartCoroutine(movePanel());
    }

    IEnumerator movePanel()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();

            panelRectTransform.localPosition = new Vector2(0, Mathf.Lerp(panelRectTransform.localPosition.y, targetPositionOfPopupPanel.y, 0.0001f * popupShowSpeed));


            if(panelRectTransform.localPosition.y + minDistanceForMoveDirectTargetPosition >=  targetPositionOfPopupPanel.y)
            {
                panelRectTransform.localPosition = targetPositionOfPopupPanel;
                Debug.Log("Popup Hedefe Ulaþýldý");
                break;
                
            }
        }
    }



}
