using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(5);
    }
}
