using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private float maxSizeBoss;

    [SerializeField] private float magnificationSizeSpeed;

    [SerializeField] private float magnificationSizeEachIngestCell;

    private bool reachMaxSize = false;

    private float targetSize;


    void Start()
    {
        
    }


    void Update()
    {
        if(transform.localScale.x < targetSize && reachMaxSize == false)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f) * magnificationSizeSpeed * Time.deltaTime;
        }

        if(transform.localScale.x >= maxSizeBoss)
        {
            reachMaxSize = true;
        }
    }


    public void magnificationBoss()
    {
        targetSize += magnificationSizeEachIngestCell;
    }


}
