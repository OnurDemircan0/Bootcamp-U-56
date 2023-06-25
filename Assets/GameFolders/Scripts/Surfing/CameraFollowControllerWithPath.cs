using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;
using StarterAssets;

public class CameraFollowControllerWithPath : MonoBehaviour
{

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float dstTravelled;

    public float speed = 5.0f;
    public float maxSpeed = 50.0f;
    public float speedAcceleration = 0.25f;

    public Transform character;
    public float distance;

    [SerializeField] private StarterAssetsInputs starterAssetsInputs;

    [SerializeField] private float maximumRotationValueX;
    [SerializeField] private float maximumRotationValueY;

    private float mouseX;
    private float mouseY;

    private float sumMouseX;
    private float sumMouseY;

    private void Start()
    {
        distance = character.position.z - transform.position.z; // 4 metre iyi
    }


    void LateUpdate()
    {
        Debug.Log("dstTravelled: " + dstTravelled);

        followPath();
    }

    private void followPath()
    {
        if (dstTravelled > 780 - 4 && speed < maxSpeed)
        {
            speed += speedAcceleration * Time.deltaTime;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

        dstTravelled += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);

        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + 1.5f
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z); 

        Debug.Log("Camera Rotatinon: " + transform.rotation.eulerAngles);
        //Debug.Log("Rotatinon: " + pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);



        // Get the mouse delta. This is not in the range -1...1
        //float x = speed * Input.GetAxis("Mouse X");
        //float y = speed * Input.GetAxis("Mouse Y");
        //transform.Rotate(x, y, 0);



        mouseX = starterAssetsInputs.look.x;
        mouseY = starterAssetsInputs.look.y;

        Debug.Log("mouseX: " + mouseX);
        Debug.Log("mouseY: " + mouseY);

        sumMouseX += mouseX;
        sumMouseY += mouseY;

        if(sumMouseX > maximumRotationValueX)
        {
            sumMouseX = maximumRotationValueX;
        }
        else if (sumMouseX < maximumRotationValueX * -1)
        {
            sumMouseX = maximumRotationValueX * -1;
        }

        if (sumMouseY > maximumRotationValueY)
        {
            sumMouseY = maximumRotationValueY;
        }
        else if (sumMouseY < maximumRotationValueY * -1)
        {
            sumMouseY = maximumRotationValueY * -1;
        }

        Debug.Log("sumMouseX: " + sumMouseX);
        Debug.Log("sumMouseY: " + sumMouseY);



        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 10 + sumMouseY
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.y + sumMouseX
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.z + 90));

        //transform.rotation = Quaternion.Euler(new Vector3(270,0,0));
        /*
        transform.rotation = Quaternion.Euler(new Vector3(pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x + 270
            , -90
            , pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles.x - 270));
        */
    }
}
