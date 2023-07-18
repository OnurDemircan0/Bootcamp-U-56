using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastVirusCount : MonoBehaviour
{
    public static int lastVirusCount;
    [SerializeField] GameObject finish;
    private bool doOnce;
    private void Start()
    {
        lastVirusCount = 0;
        doOnce = true;
    }

    private void Update()
    {
        if(lastVirusCount >= 22)
        {
            if (doOnce)
            {
                finish.SetActive(true);
            }
        }
        Debug.Log(lastVirusCount);
    }

}
