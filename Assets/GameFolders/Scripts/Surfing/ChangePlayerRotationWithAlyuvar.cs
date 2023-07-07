using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerRotationWithAlyuvar : MonoBehaviour
{
    [SerializeField] private GameObject surfAlyuvarTable;


    private float firstPlayerRotationAccordingToAlyuvarX;
    private float firstPlayerRotationAccordingToAlyuvarY;
    private float firstPlayerRotationAccordingToAlyuvarZ;

    void Start()
    {
        Invoke("determineFirstPlayerTransformAccordingToAlyuvar", 0.03f);
    }


    private void determineFirstPlayerTransformAccordingToAlyuvar()
    {


        firstPlayerRotationAccordingToAlyuvarX = surfAlyuvarTable.transform.eulerAngles.x - transform.eulerAngles.x;
        firstPlayerRotationAccordingToAlyuvarY = surfAlyuvarTable.transform.eulerAngles.y - transform.eulerAngles.y;
        firstPlayerRotationAccordingToAlyuvarZ = surfAlyuvarTable.transform.eulerAngles.z - transform.eulerAngles.z;

        Debug.Log("firstPlayerRotationAccordingToAlyuvarEulerAngles: " + surfAlyuvarTable.transform.eulerAngles);

        Debug.Log("firstPlayerRotationAccordingToAlyuvarX: " + firstPlayerRotationAccordingToAlyuvarX);
        Debug.Log("firstPlayerRotationAccordingToAlyuvarY: " + firstPlayerRotationAccordingToAlyuvarY);
        Debug.Log("firstPlayerRotationAccordingToAlyuvarZ: " + firstPlayerRotationAccordingToAlyuvarZ);
    }


    void Update()
    {

        //transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

        /*
        transform.rotation = Quaternion.Euler(new Vector3(surfAlyuvarTable.transform.eulerAngles.x + firstPlayerRotationAccordingToAlyuvarX,
            surfAlyuvarTable.transform.eulerAngles.y + firstPlayerRotationAccordingToAlyuvarY,
            surfAlyuvarTable.transform.eulerAngles.z + firstPlayerRotationAccordingToAlyuvarZ));
        */


        transform.rotation = Quaternion.Euler(new Vector3(surfAlyuvarTable.transform.eulerAngles.x + firstPlayerRotationAccordingToAlyuvarX,
            transform.eulerAngles.y,
            surfAlyuvarTable.transform.eulerAngles.z + firstPlayerRotationAccordingToAlyuvarZ));

        /*
        transform.rotation = new Vector3(surfAlyuvarTable.transform.position.x + firstPlayerRotationAccordingToAlyuvarX
            , surfAlyuvarTable.transform.position.y + firstPlayerRotationAccordingToAlyuvarY
            , surfAlyuvarTable.transform.position.z + firstPlayerRotationAccordingToAlyuvarZ);
        */

    }
}
