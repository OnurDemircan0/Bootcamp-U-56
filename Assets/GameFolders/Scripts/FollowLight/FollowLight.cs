using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLight : MonoBehaviour
{
    [SerializeField] GameObject followLight;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            followLight.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            followLight.SetActive(false);
        }
    }
}
