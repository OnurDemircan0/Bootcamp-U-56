using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class OtherAlyuvarController : MonoBehaviour
{

    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction end;
    float dstTravelled;


    private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float maxRotationAngle;

    [SerializeField] private float maxDistanceFromPathPoint = 1.25f;

    [SerializeField] private float minDistanceFromStartPoint;
    [SerializeField] private float maxDistanceFromStartPoint;

    private float randomPositionX;
    private float randomPositionY;
    private float randomPositionZ;


    private float randomRotationX;
    private float randomRotationY;
    private float randomRotationZ;

    void Start()
    {
        if(pathCreator == null)
        {
            Debug.Log("pathCreator null");
            pathCreator = GameObject.Find("path").gameObject.GetComponent<PathCreator>();
        }

        randomPositionX = Random.Range(maxDistanceFromPathPoint * -1, maxDistanceFromPathPoint);
        randomPositionY = Random.Range(maxDistanceFromPathPoint * -1, maxDistanceFromPathPoint);
        //randomPositionZ = Random.Range(minDistanceFromStartPoint, maxDistanceFromStartPoint);

        randomRotationX = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationY = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationZ = Random.Range(maxRotationAngle * -1, maxRotationAngle);

        speed = Random.Range(minSpeed, maxSpeed);

        dstTravelled = Random.Range(minDistanceFromStartPoint, maxDistanceFromStartPoint);

    }

    void Update()
    {
        followPath();
    }

    private void followPath()
    {
        dstTravelled += speed * Time.deltaTime;
        //transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);




        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + randomPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + randomPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z + randomPositionZ);


        transform.Rotate(randomRotationX, randomRotationY, randomRotationZ, Space.Self);


        //Debug.Log("Character Rotatinon: " + transform.rotation.eulerAngles);
        //Debug.Log("Rotatinon: " + pathCreator.path.GetRotationAtDistance(dstTravelled, end).eulerAngles);

        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);



    }
}
