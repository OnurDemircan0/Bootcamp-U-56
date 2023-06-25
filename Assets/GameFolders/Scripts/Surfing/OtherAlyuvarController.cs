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
    public float speedAcceleration = 0.25f;
    public float maxSpeed;
    public float minSpeedForRandom; 
    public float maxSpeedForRandom;
    [SerializeField] private float maxRotationAngle;

    public float minDistanceFromPathPoint = 0.0f;
    public float maxDistanceFromPathPoint = 0.5f;
    

    public float minDistanceFromStartPoint;
    public float maxDistanceFromStartPoint;

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

        //randomPositionX = Random.Range(maxDistanceFromPathPoint * -1, maxDistanceFromPathPoint);
        //randomPositionY = Random.Range(maxDistanceFromPathPoint * -1, maxDistanceFromPathPoint);
        //randomPositionZ = Random.Range(minDistanceFromStartPoint, maxDistanceFromStartPoint);

        if(Random.Range(0,2) == 0)
        {
            randomPositionX = Random.Range(maxDistanceFromPathPoint * -1, minDistanceFromPathPoint * -1);
        }
        else
        {
            randomPositionX = Random.Range(minDistanceFromPathPoint, maxDistanceFromPathPoint);
        }

        if (Random.Range(0, 2) == 0)
        {
            randomPositionY = Random.Range(maxDistanceFromPathPoint * -1, minDistanceFromPathPoint * -1);
            
        }
        else
        {
            randomPositionY = Random.Range(minDistanceFromPathPoint, maxDistanceFromPathPoint);
        }

        randomRotationX = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationY = Random.Range(maxRotationAngle * -1, maxRotationAngle);
        randomRotationZ = Random.Range(maxRotationAngle * -1, maxRotationAngle);

        speed = Random.Range(minSpeedForRandom, maxSpeedForRandom);

        dstTravelled = Random.Range(minDistanceFromStartPoint, maxDistanceFromStartPoint);

    }

    void Update()
    {
        followPath();
    }

    private void followPath()
    {
        if (dstTravelled > 780 && speed < maxSpeed)
        {
            speed += speedAcceleration * Time.deltaTime;
        }
        else if (speed >= maxSpeed)
        {
            speed = maxSpeed;
        }

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
