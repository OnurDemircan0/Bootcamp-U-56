using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class CameraFollowControllerWithPath : MonoBehaviour
{

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float dstTravelled;

    public float speed = 5.0f;

    public Transform character;
    public float distance;

    private void Start()
    {
        distance = character.position.z - transform.position.z;
    }


    void LateUpdate()
    {
        Debug.Log("dstTravelled: " + dstTravelled);

        followPath();
    }

    private void followPath()
    {
        dstTravelled += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);

        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + 1
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z - 1f); 

        Debug.Log("Camera Rotatinon: " + transform.rotation.eulerAngles);
        //Debug.Log("Rotatinon: " + pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);

        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 10
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z + 90));

        //transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
        /*
        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 270
            , -90
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x - 270));
        */
    }
}
