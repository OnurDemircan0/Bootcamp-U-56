using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainVirusArea : MonoBehaviour
{
    private Animator _animator;
    private int stage;
    private int nextVirusCount;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        stage = 0;
        nextVirusCount = 0;
    }

    private void Update()
    {
        switch (stage)
        {
            case 0:
                break;
            case 1:
                nextVirusCount = 3;
                break;
            case 2:
                nextVirusCount = 10;
                break;
            case 3:
                nextVirusCount = 15;
                break;
            case 4:
                nextVirusCount = 24;
                break;
            case 5:
                nextVirusCount = 32;
                break;
            case 6:
                nextVirusCount = 49;
                break;
        }
        Debug.Log("Virus Count: " + VirusCount.virusCount + " , NextVirusCount: " + nextVirusCount);
        if(VirusCount.virusCount >= nextVirusCount && PlayerCheckingArea.playerDetectedMainVirusArea)
        {
            _animator.SetTrigger("NextStage");
            stage++;
        }


        
    }
}
