using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private GameObject settingsPanel;

    private bool settingsPanelOpenControl = false;

    private void Awake()
    {
        settingsPanel.SetActive(settingsPanelOpenControl);
    }

    public void setVolume(float volume)
    {
        Debug.Log("Volume: " + volume);

        audioMixer.SetFloat("volume", volume);
    }

    public void clickedSettings()
    {
        settingsPanelOpenControl = !settingsPanelOpenControl;

        if (settingsPanelOpenControl)
        {
            settingsPanel.SetActive(true);
        }
        else
        {
            settingsPanel.SetActive(false);
        }
    }


}
