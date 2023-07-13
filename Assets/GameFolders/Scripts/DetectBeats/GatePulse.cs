using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePulse : MonoBehaviour
{
    public Vector3 pulsePosition;

    Vector3 originalPosition;

    [SerializeField]
    float pulseVelocity = 1f;

    public bool pulseStart = false;

    [SerializeField]
    BeatDetection beatDetect;


    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pulseVelocity);
    }

    public void OnTriggerParent()
    {
        Invoke("SetPulseStart", 2f);
        Debug.Log("Entered Collider");
    }

    void SetPulseStart()
    {
        pulseStart = true;
        StartPassagePulse();
    }

    public void StartPassagePulse()
    {
        if (pulseStart)
        {
            transform.localPosition = pulsePosition;
        }
    }
}
