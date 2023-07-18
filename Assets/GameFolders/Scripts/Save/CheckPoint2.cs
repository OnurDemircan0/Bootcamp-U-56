using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint2 : MonoBehaviour
{
    [SerializeField] private int checkPointNumberSelf = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckPointSystem.checkPointNumber = checkPointNumberSelf;
            PlayerPrefs.SetInt("CheckPointNumber", checkPointNumberSelf);
            PlayerPrefs.Save();
        }
    }
}
