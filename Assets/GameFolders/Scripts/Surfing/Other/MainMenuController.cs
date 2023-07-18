using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI continueButton;

    [SerializeField] private GameObject credits;
    private GameObject virus;



    private bool creditsOpenControl = false;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("ContinueGame") == 0)
            continueButton.alpha = 0.2f;


        
    }

    private void Start()
    {
        virus = GameObject.FindGameObjectWithTag("Enemy");
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
        //En son kay�t yap�lan sahneye git
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

    public void showOrHideCredits()
    {
        creditsOpenControl = !creditsOpenControl;

        if (creditsOpenControl)
        {
            credits.SetActive(true);
            virus.SetActive(false);
        }
        else
        {
            credits.SetActive(false);
            virus.SetActive(true);
        }
    }

}
