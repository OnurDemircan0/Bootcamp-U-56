using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void goFirstScene()
    {
        //SceneManager.LoadScene("Vein_Scene");
        SceneManager.LoadScene(1);
    }

    public void continueScene()
    {
        //En son kayýt yapýlan sahneye git
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
