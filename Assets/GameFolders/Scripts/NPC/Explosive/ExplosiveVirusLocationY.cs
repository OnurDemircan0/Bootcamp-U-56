using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveVirusLocationY : MonoBehaviour
{
    private void Start()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 66.6f, transform.localPosition.z);
    }
    private void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localScale.y / 66.6f, transform.localPosition.z);
    }
}
