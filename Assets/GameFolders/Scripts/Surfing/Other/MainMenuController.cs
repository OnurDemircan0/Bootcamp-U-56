using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI continueButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ContinueGame") == 0)
            continueButton.alpha = 0.2f;
    }

    public void goFirstScene()
    {
        //SceneManager.LoadScene("Vein_Scene");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("ContinueGame", 1);
        SceneManager.LoadScene(1);
    }

    public void continueScene()
    {
        //En son kayýt yapýlan sahneye git
        if(PlayerPrefs.GetInt("ContinueGame") != 0)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("LastBuildIndex", 1));
        }
    }

    public void goSecene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }
}
