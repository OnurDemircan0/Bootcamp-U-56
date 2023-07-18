using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointHeart : MonoBehaviour
{
    [SerializeField] private int checkPointNumberSelf = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointSystemHeart.checkPointNumberHeart = checkPointNumberSelf;
            PlayerPrefs.SetInt("CheckPointNumberHeart", checkPointNumberSelf);
            PlayerPrefs.Save();
        }
    }
}
