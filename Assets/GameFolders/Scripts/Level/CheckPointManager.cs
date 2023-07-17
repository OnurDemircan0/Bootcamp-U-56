using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    Transform Player;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (CheckPoint.lastCheckPointPosition != Vector3.zero)
        {
            Player.position = CheckPoint.lastCheckPointPosition;

            /*
            if(SceneManager.GetActiveScene().name == "Onur_TestScenePathTest")
            {
                PlayerPrefs.SetFloat("BrainStartingPositionX", Player.position.x);
                PlayerPrefs.SetFloat("BrainStartingPositionY", Player.position.y);
                PlayerPrefs.SetFloat("BrainStartingPositionZ", Player.position.z);
                PlayerPrefs.Save();
            }
            else if(SceneManager.GetActiveScene().name == "Kalp")
            {
                
            }
            else
            {
                Debug.Log("olamaaaAAAAAAAAZ");
            }
            */
        }
    }
}