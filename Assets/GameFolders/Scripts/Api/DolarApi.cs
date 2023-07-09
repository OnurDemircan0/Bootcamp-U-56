using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DolarApi : MonoBehaviour
{

    private string dolarApiUrl = "https://v6.exchangerate-api.com/v6/9a6f7debe2c7311c123ed659/latest/USD";

    private string answerFromApi = "";

    private string usdTryValue = "26.07"; //Ýnternet Yoksa veya baðlantý hatasý varsa döndürülecek deðer

    void Awake()
    {
        StartCoroutine(sendRequest(dolarApiUrl));
    }

    public string getUsdToTlInfo()
    {
        usdTryValue = answerFromApi.Split("TRY\":")[1].Split(",")[0];
        Debug.Log(usdTryValue);

        return usdTryValue;

    }

    IEnumerator sendRequest(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            answerFromApi = www.downloadHandler.text;
            Debug.Log(answerFromApi);

            getUsdToTlInfo();


            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }
}
