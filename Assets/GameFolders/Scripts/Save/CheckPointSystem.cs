using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystem : MonoBehaviour
{
    public static int checkPointNumber;

    [SerializeField] Transform[] checkPoints;
    [SerializeField] Transform player;

    private void Awake()
    {
        checkPointNumber = PlayerPrefs.GetInt("CheckPointNumber", 0);
    }

    private void Start()
    {
        //player.position = checkPoints[checkPointNumber].position;
        player.SetPositionAndRotation(checkPoints[checkPointNumber].position, checkPoints[checkPointNumber].rotation);
    }
}
