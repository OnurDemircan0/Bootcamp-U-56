using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicColor : MonoBehaviour
{
    [SerializeField] Material[] mimicColors;
    private Renderer _renderer;
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        int randomColor = Random.Range(0, 3);
        _renderer.material = mimicColors[randomColor];
    }
}
