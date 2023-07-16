using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;

public class FollowPath : MonoBehaviour
{

    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    public float dstTravelled;


    [SerializeField] private float addPositionX;
    [SerializeField] private float addPositionY;
    [SerializeField] private float addPositionZ;

    public float speed = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dstTravelled += speed * Time.deltaTime;
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.position += new Vector3(addPositionX, addPositionY, addPositionZ);
    }
}
