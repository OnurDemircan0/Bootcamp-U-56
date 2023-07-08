using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticsController : MonoBehaviour, IGameAnalyticsATTListener
{
    void Start()
    {
        Debug.Log("1");

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            Debug.Log("2");
            GameAnalytics.RequestTrackingAuthorization(this);
            Debug.Log("3");

            GameAnalytics.Initialize();
            Debug.Log("6");
        }
        else
        {
            Debug.Log("4");
            GameAnalytics.Initialize();
            Debug.Log("5");
        }
    }

    public void GameAnalyticsATTListenerNotDetermined()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerRestricted()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerDenied()
    {
        GameAnalytics.Initialize();
    }
    public void GameAnalyticsATTListenerAuthorized()
    {
        GameAnalytics.Initialize();
    }
}
