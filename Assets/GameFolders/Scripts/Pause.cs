using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject PauseMenuPaneli;
    public bool PaneliAcKapat;

    private void Start()
    {
        PauseMenuPaneli = GameObject.FindGameObjectWithTag("PauseMenu"); // PauseMenu tag'ini kullanmalýsýnýz
        PauseMenuPaneli.SetActive(false);
    }

    private void Update()
    {
        OyunuDurdur();
    }

    private void OyunuDurdur()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PaneliAcKapat = !PaneliAcKapat;
            if (PaneliAcKapat)
            {
                Time.timeScale = 0;
                PauseMenuPaneli.SetActive(true);

                AudioSource[] audioSources = FindObjectsOfType<AudioSource>();


                foreach(AudioSource a in audioSources)
                {
                    a.Stop();
                }
            }
            else
            {
                Time.timeScale = 1;
                PauseMenuPaneli.SetActive(false);

                AudioSource[] audioSources = FindObjectsOfType<AudioSource>();


                foreach (AudioSource a in audioSources)
                {
                    a.Stop();
                }
            }
        }
    }

    public void DevamEt()
    {
        PaneliAcKapat = false;
        PauseMenuPaneli.SetActive(false);
        Time.timeScale = 1;
    }


    public void goMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
