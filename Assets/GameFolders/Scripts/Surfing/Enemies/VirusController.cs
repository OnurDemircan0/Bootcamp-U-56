using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class VirusController : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;
    [SerializeField] private EndOfPathInstruction end;
    public float dstTravelled;


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


    public float nowPositionX = 0.0f;
    public float nowPositionY = 0.0f;
    public float nowPositionZ = 0.0f;


    public float addPositionX = 0.0f;
    public float addPositionY = 0.0f;
    public float addPositionZ = 0.0f;

    private float randomPositionX;
    private float randomPositionY;

    private float addPositionBeforeValueX = 0.0f;
    private float addPositionBeforeValueY = 0.0f;
    private float addPositionBeforeValueZ = 0.0f;

    void Start()
    {
        if (pathCreator == null)
        {
            Debug.Log("pathCreator null");

            pathCreator = GameObject.Find("path for enemy").gameObject.GetComponent<PathCreator>();
        }

        nowPositionX = transform.position.x - pathCreator.path.GetPointAtDistance(dstTravelled, end).x;
        nowPositionY = transform.position.y - pathCreator.path.GetPointAtDistance(dstTravelled, end).y;
        nowPositionZ = transform.position.z - pathCreator.path.GetPointAtDistance(dstTravelled, end).z;

        Debug.Log("nowPositionX: " + nowPositionX);
        Debug.Log("nowPositionY: " + nowPositionY);
        Debug.Log("nowPositionZ: " + nowPositionZ);


        speed = Random.Range(minSpeedForRandom, maxSpeedForRandom);

        dstTravelled = Random.Range(minDistanceFromStartPoint, maxDistanceFromStartPoint);



        if (Random.Range(0, 2) == 0)
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

        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + nowPositionX + addPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + nowPositionY + addPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z + nowPositionZ + addPositionZ);
        */

        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x  + addPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y  + addPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z + addPositionZ);
        */

        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + nowPositionX + addPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + nowPositionY + addPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z + nowPositionZ + addPositionZ);
        */


        /*
        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + nowPositionX + addPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + nowPositionY + addPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);
        */


        transform.position = new Vector3(pathCreator.path.GetPointAtDistance(dstTravelled, end).x + nowPositionX + addPositionX + randomPositionX
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).y + nowPositionY + addPositionY + randomPositionY
            , pathCreator.path.GetPointAtDistance(dstTravelled, end).z);


        if (addPositionBeforeValueX != addPositionX)
        {
            addPositionBeforeValueX = addPositionX;
        }
        else
        {
            nowPositionX += addPositionX;

            addPositionBeforeValueX = 0;
            addPositionX = 0;
        }

        if (addPositionBeforeValueY != addPositionY)
        {
            addPositionBeforeValueY = addPositionY;
        }
        else
        {
            nowPositionY += addPositionY;

            addPositionBeforeValueY = 0;
            addPositionY = 0;
        }

        if (addPositionBeforeValueZ != addPositionZ)
        {
            addPositionBeforeValueZ = addPositionZ;
        }
        else
        {
            nowPositionZ += addPositionZ;

            addPositionBeforeValueZ = 0;
            addPositionZ = 0;
        }

    }
}
