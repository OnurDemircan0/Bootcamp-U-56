using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
            else
            {
                Time.timeScale = 1;
                PauseMenuPaneli.SetActive(false);
            }
        }
    }

    public void DevamEt()
    {
        Time.timeScale = 1;
        PaneliAcKapat = false;
        PauseMenuPaneli.SetActive(false);
    }
}
