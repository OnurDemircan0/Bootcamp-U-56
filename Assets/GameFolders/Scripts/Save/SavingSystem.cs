using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingSystem : MonoBehaviour
{
    private int lastBuildIndex = 0;

    private void Awake()
    {
        lastBuildIndex = PlayerPrefs.GetInt("LastBuildIndex", lastBuildIndex);
    }

    private void OnApplicationQuit()
    {
        lastBuildIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("LastBuildIndex", lastBuildIndex);
        PlayerPrefs.Save();
    }

    public void StartLastScene()
    {
        SceneManager.LoadScene(lastBuildIndex);
    }
}
