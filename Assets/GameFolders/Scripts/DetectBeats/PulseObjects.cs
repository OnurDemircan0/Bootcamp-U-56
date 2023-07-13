using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseObjects : MonoBehaviour
{
    Vector3 startScale;

    [SerializeField]
    float pulseScale = 1.1f;

    public
    float pulseScalePassage = 1.1f;

    [SerializeField]
    float pulseTime = 5f;

    private void Start()
    {
        startScale = transform.localScale;
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startScale, Time.deltaTime * pulseTime);
    }

    public void PulseVein()
    {
        Vector3 newScale = transform.localScale;
        newScale.y *= pulseScale;
        transform.localScale = newScale;
    }

    public void PulsePassage()
    {
        Vector3 newScale = transform.localScale;
        newScale *= pulseScalePassage;
        transform.localScale = newScale;
    }
}
