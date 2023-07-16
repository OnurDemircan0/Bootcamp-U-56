using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOtherAlyuvarsComeFromBehind : MonoBehaviour
{
    public bool createOtherAlyuvarControl;

    [SerializeField] private GameObject otherAlyuvars;
    [SerializeField] private SurfingControllerV2 surfingControllerV2;

    [SerializeField] private float minFarFromCharacter;
    [SerializeField] private float maxFarFromCharacter;

    [SerializeField] private float minDistanceFromPathPoint = 1.5f;
    [SerializeField] private float maxDistanceFromPathPoint = 4.0f;
    


    [SerializeField] private float maxPlusSpeed;
    [SerializeField] private float minPlusSpeed;

    [SerializeField] private int maxOtherAlyuvarsCountEachCreate;


    [SerializeField] private float minBetweenTimeEachCreate;
    [SerializeField] private float maxBetweenTimeEachCreate;


    void Start()
    {

        StartCoroutine(createAlyuvars());
    }

    IEnumerator createAlyuvars()
    {
        while (true)
        {
            if (createOtherAlyuvarControl)
            {
                yield return new WaitForSeconds(Random.Range(minBetweenTimeEachCreate, maxBetweenTimeEachCreate));

                for (int i = 0; i < Random.Range(1, maxOtherAlyuvarsCountEachCreate); i++)
                {
                    OtherAlyuvarController otherAlyuvarComeFromBehind = Instantiate(otherAlyuvars, transform.position, transform.rotation).GetComponent<OtherAlyuvarController>();

                    otherAlyuvarComeFromBehind.minDistanceFromStartPoint = surfingControllerV2.dstTravelled - maxFarFromCharacter;
                    otherAlyuvarComeFromBehind.maxDistanceFromStartPoint = surfingControllerV2.dstTravelled - minFarFromCharacter;

                    otherAlyuvarComeFromBehind.minSpeedForRandom = surfingControllerV2.speed + minPlusSpeed;
                    otherAlyuvarComeFromBehind.maxSpeedForRandom = surfingControllerV2.speed + maxPlusSpeed;


                    otherAlyuvarComeFromBehind.minDistanceFromPathPoint = minDistanceFromPathPoint;
                    otherAlyuvarComeFromBehind.maxDistanceFromPathPoint = maxDistanceFromPathPoint;
                }
            }
            else
            {
                break;
            }
            
            
        }
        
    }
}
