using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawning : MonoBehaviour
{
    private GameObject _mimic;
    private float deadTime;
    private float spawningTime = 10f;

    private void Start()
    {
        deadTime = 0;
        _mimic = transform.GetChild(0).gameObject;
    }
    private void Update()
    {
        if (!_mimic.activeSelf)
        {
            deadTime += Time.deltaTime;

            if(deadTime > spawningTime)
            {
                _mimic.SetActive(true);
            }
        }
    }
}
