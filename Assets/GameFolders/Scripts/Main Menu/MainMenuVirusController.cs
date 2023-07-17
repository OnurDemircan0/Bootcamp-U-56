using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuVirusController : MonoBehaviour
{
    [SerializeField] private GameObject[] viruses;

    private int virusNumber = 0;

    void Awake()
    {
        virusNumber = Random.Range(0, viruses.Length);

        viruses[virusNumber].SetActive(true);
    }


}
