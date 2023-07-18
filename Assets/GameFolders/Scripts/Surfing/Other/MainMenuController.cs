using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject continueButton;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ContinueGame") == 0)
            continueButton.SetActive(false);
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
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastBuildIndex", 1));
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
