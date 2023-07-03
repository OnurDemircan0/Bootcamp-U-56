using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBotLocation : MonoBehaviour
{
    public Transform nextLocation;
    float chatBotTimer;
    bool canMove;
    // Start is called before the first frame update
    void Start()
    {
        chatBotTimer = 0;
        canMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            canMove = true;

        }

        if (canMove)
        {
            transform.position = Vector3.Lerp(transform.position, nextLocation.position, chatBotTimer);
            chatBotTimer += Time.deltaTime;
        }
    }
}
