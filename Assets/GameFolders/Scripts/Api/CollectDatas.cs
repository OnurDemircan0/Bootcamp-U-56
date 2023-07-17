using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class CollectDatas : MonoBehaviour
{
    private string mainLink = "https://g143f7239a.execute-api.eu-west-1.amazonaws.com/default/BootcampSavePhoneInfos";
                                
    private string fullLink = "";

    private string beforeSceneName = "";
    private string nowSceneName = "";


    private string appName = "BootcampMedBotGame";

    private int numberOfEnteringToApp;

    private long userId = 0; //999.999.999.999.999.999


    private string systemLanguage;
    private string firstBatteryStatus;
    private string lastBatteryStatus;

    private string deviceModel;
    private string deviceName;
    private string deviceType;
    private string graphicsDeviceName;
    private string graphicsDeviceType;
    private string graphicsDeviceVendor;
    private string graphicsDeviceVersion;
    private string graphicsMemorySize;
    private string operatingSystem;
    private string operatingSystemFamily;
    private string processorCount;
    private string processorFrequency;
    private string processorType;
    private string systemMemorySize;

    private int firstBatteryLevel;
    private int lastBatteryLevel;

    private float spentTime;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        beforeSceneName = SceneManager.GetActiveScene().name;
        nowSceneName = SceneManager.GetActiveScene().name;
    }


    void Start()
    {
        createUserId();

        updateNumberOfEnteringToApp();

        if(numberOfEnteringToApp <= 1)
        {
            getNotChangeableDatas();
        }
        

        getChangeableFirstDatas();
        InvokeRepeating("getChangeableLastDatas", 1, 2);


        InvokeRepeating("controlChangeScene", 0.25f, 0.5f);



        
    }

    private void updateNumberOfEnteringToApp()
    {
        numberOfEnteringToApp = PlayerPrefs.GetInt("numberOfEnteringToApp", 1);

        PlayerPrefs.SetInt("numberOfEnteringToApp", numberOfEnteringToApp + 1);
    }

    private void createUserId()
    {
        userId = long.Parse(PlayerPrefs.GetString("userId", "0"));
        if(userId == 0)
        {
            long random1 = UnityEngine.Random.Range(100000000, 999999999);
            long random2 = UnityEngine.Random.Range(100000000, 999999999);

            userId = random1 * random2;

            PlayerPrefs.SetString("userId", userId.ToString());

            //print(userId);

            //print("Random1: " + random1 + " Random2: " + random2 + " Result: " + userId);
        }

        print(userId);
    }

    IEnumerator sendRequest(string url)
    {
        //Debug.Log(url);

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);

            // Or retrieve results as binary data
            //byte[] results = www.downloadHandler.data;
        }
    }

    private void getNotChangeableDatas()
    {
        try
        {
            systemLanguage = Application.systemLanguage.ToString();

            deviceModel = SystemInfo.deviceModel.ToString();
            deviceName = SystemInfo.deviceName.ToString();
            deviceType = SystemInfo.deviceType.ToString();
            graphicsDeviceName = SystemInfo.graphicsDeviceName.ToString();
            graphicsDeviceType = SystemInfo.graphicsDeviceType.ToString();
            graphicsDeviceVendor = SystemInfo.graphicsDeviceVendor.ToString();
            graphicsDeviceVersion = SystemInfo.graphicsDeviceVersion.ToString();
            graphicsMemorySize = SystemInfo.graphicsMemorySize.ToString();
            operatingSystem = SystemInfo.operatingSystem.ToString();
            operatingSystemFamily = SystemInfo.operatingSystemFamily.ToString();
            processorCount = SystemInfo.processorCount.ToString();
            processorFrequency = SystemInfo.processorFrequency.ToString();
            processorType = SystemInfo.processorType.ToString();
            systemMemorySize = SystemInfo.systemMemorySize.ToString();

            fullLink = mainLink + "?appName=" + appName +
                "&userId=" + userId + " - General Device Infos" +

                "&systemLanguage=" + systemLanguage +

                "&deviceModel=" + deviceModel +
                "&deviceName=" + deviceName +
                "&deviceType=" + deviceType +

                "&graphicsDeviceName=" + graphicsDeviceName +
                "&graphicsDeviceType=" + graphicsDeviceType +
                "&graphicsDeviceVendor=" + graphicsDeviceVendor +
                "&graphicsDeviceVersion=" + graphicsDeviceVersion +
                "&graphicsMemorySize=" + graphicsMemorySize +

                "&operatingSystem=" + operatingSystem +
                "&operatingSystemFamily=" + operatingSystemFamily +

                "&processorCount=" + processorCount +
                "&processorFrequency=" + processorFrequency +
                "&processorType=" + processorType +

                "&systemMemorySize=" + systemMemorySize

                ;



            //print(fullLink);
            StartCoroutine(sendRequest(fullLink));



            /*
            print(SystemInfo.supportsGyroscope);
            print(SystemInfo.supportsRayTracing);
            print(SystemInfo.supportsShadows);
            print(SystemInfo.supportsVibration);
            */

        }
        catch (Exception e)
        {

        }


    }

    private void controlChangeScene()
    {
        nowSceneName = SceneManager.GetActiveScene().name;

        //Debug.Log("nowSceneName: " + nowSceneName);
        //Debug.Log("beforeSceneName: " + beforeSceneName);

        if(nowSceneName != beforeSceneName)
        {
            //Debug.Log("nowSceneName ile beforeSceneName birbirinden farklý");

            getChangeableFirstDatas();

            beforeSceneName = nowSceneName;
        }
    }

    private void getChangeableFirstDatas()
    {
        firstBatteryLevel = (int)(SystemInfo.batteryLevel * 100);

        firstBatteryStatus = SystemInfo.batteryStatus.ToString();

        fullLink = mainLink + "?appName=" + appName +
                "&userId=" + userId + " - " + numberOfEnteringToApp.ToString() + " Level Name=" + SceneManager.GetActiveScene().name  + " - First Received Data" +

                "&firstBatteryLevel=" + firstBatteryLevel +
                "&firstBatteryStatus=" + firstBatteryStatus +
                "&spentTime=" + spentTime

                ;

        //print(fullLink);

        StartCoroutine(sendRequest(fullLink));

    }

    private void getChangeableLastDatas()
    {
        lastBatteryLevel = (int)(SystemInfo.batteryLevel * 100);

        lastBatteryStatus = SystemInfo.batteryStatus.ToString();

        spentTime = Time.time;

        fullLink = mainLink + "?appName=" + appName +
                "&userId=" + userId + " - " + numberOfEnteringToApp.ToString() + " Level Name=" + SceneManager.GetActiveScene().name  + " - Last Received Data" +

                "&lastBatteryLevel=" + lastBatteryLevel +
                "&lastBatteryStatus=" + lastBatteryStatus +
                "&spentTime=" + spentTime

                ;

        //print(fullLink);

        StartCoroutine(sendRequest(fullLink));

    }



}
