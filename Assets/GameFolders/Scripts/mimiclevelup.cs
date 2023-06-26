using MimicSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimiclevelup : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("mimic"))
        {
            other.gameObject.transform.localScale= new Vector3(2, 2, 2);
            other.GetComponent<Mimic>().legCount = 30;
        }
    }
}
