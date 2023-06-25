using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityReset : MonoBehaviour
{
    ThirdPersonController personController;
    Slide slidecs;

    private void OnTriggerEnter(Collider other)
    {
        personController.Grounded = true;
        slidecs.Sliding = false;
        
    }
    void Start()
    {
        personController = GetComponent<ThirdPersonController>();
        slidecs = GetComponent<Slide>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
