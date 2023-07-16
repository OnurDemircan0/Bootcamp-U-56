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
        }
    }
}