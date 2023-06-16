using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowController : MonoBehaviour
{
    public Transform character;
    public float distance;

    private void Start()
    {
        distance = character.position.z - transform.position.z;
        Debug.Log("Distance: " + distance);
    }

    private void LateUpdate()
    {
        // Kameray� takip edilecek nesnenin pozisyonuna do�ru hareket ettirme
        transform.position = new Vector3(transform.position.x, transform.position.y, character.position.z - character.forward.z * distance);

        // Kameray� takip edilecek nesneye do�ru �evirme
        //transform.LookAt(character);
    }
}
