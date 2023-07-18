using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointSystemHeart : MonoBehaviour
{
    public static int checkPointNumberHeart;

    [SerializeField] Transform[] checkPoints;
    [SerializeField] GameObject player;

    private void Awake()
    {
        checkPointNumberHeart = PlayerPrefs.GetInt("CheckPointNumberHeart", 0);
    }

    private void Start()
    {
        //player.position = checkPoints[checkPointNumber].position;
        player.transform.SetPositionAndRotation(checkPoints[checkPointNumberHeart].position, checkPoints[checkPointNumberHeart].rotation);

        if(checkPointNumberHeart != 0)
        {
            player.GetComponent<SlideV2>().enabled = false;
        }
    }
}
