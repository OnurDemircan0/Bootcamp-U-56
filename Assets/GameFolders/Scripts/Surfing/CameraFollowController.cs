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
        // Kamerayý takip edilecek nesnenin pozisyonuna doðru hareket ettirme
        transform.position = new Vector3(transform.position.x, transform.position.y, character.position.z - character.forward.z * distance);

        // Kamerayý takip edilecek nesneye doðru çevirme
        //transform.LookAt(character);
    }
}
