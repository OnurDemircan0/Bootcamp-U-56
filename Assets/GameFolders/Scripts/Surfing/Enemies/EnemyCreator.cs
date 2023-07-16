using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private SurfingControllerV2 surfingControllerV2;


    public bool createVirusEnemyControl;

    [SerializeField] private float minFarFromCharacter;
    [SerializeField] private float maxFarFromCharacter;

    [SerializeField] private float minFarFromCharacterForFirstCreatingEnemies;
    [SerializeField] private float maxFarFromCharacterForFirstCreatingEnemies;

    [SerializeField] private float minDistanceFromPathPoint = 0f;
    [SerializeField] private float maxDistanceFromPathPoint = 1.5f;

    //[SerializeField] private float maxPlusSpeed;
    //[SerializeField] private float minPlusSpeed;

    [SerializeField] private float maxSubtractSpeed;
    [SerializeField] private float minSubtractSpeed;

    [SerializeField] private int maxEnemyCountEachCreate;

    [SerializeField] private int minEnemyCountForFistCreatingEnemies;
    [SerializeField] private int maxEnemyCountForFistCreatingEnemies;


    [SerializeField] private float minBetweenTimeEachCreate;
    [SerializeField] private float maxBetweenTimeEachCreate;

    private bool firstEnemyCreatingControl;


    void Awake()
    {
        firstEnemyCreatingControl = false;
        StartCoroutine(createEnemies());
    }

    IEnumerator createEnemies()
    {
        while (true)
        {
            
            if (createVirusEnemyControl)
            {
                if (firstEnemyCreatingControl == true)
                {
                    yield return new WaitForSeconds(Random.Range(minBetweenTimeEachCreate, maxBetweenTimeEachCreate));

                    for (int i = 0; i < Random.Range(1, maxEnemyCountEachCreate); i++)
                    {
                        VirusController virusController = Instantiate(enemy, transform.position, transform.rotation).GetComponent<VirusController>();

                        virusController.minDistanceFromStartPoint = surfingControllerV2.dstTravelled + maxFarFromCharacter;
                        virusController.maxDistanceFromStartPoint = surfingControllerV2.dstTravelled + minFarFromCharacter;

                        virusController.minSpeedForRandom = surfingControllerV2.speed - maxSubtractSpeed;
                        virusController.maxSpeedForRandom = surfingControllerV2.speed - minSubtractSpeed;


                        virusController.minDistanceFromPathPoint = minDistanceFromPathPoint;
                        virusController.maxDistanceFromPathPoint = maxDistanceFromPathPoint;
                    }
                }
                else
                {
                    yield return null;

                    for (int i = 0; i < Random.Range(minEnemyCountForFistCreatingEnemies, maxEnemyCountForFistCreatingEnemies); i++)
                    {
                        VirusController virusController = Instantiate(enemy, transform.position, transform.rotation).GetComponent<VirusController>();

                        virusController.minDistanceFromStartPoint = surfingControllerV2.dstTravelled + maxFarFromCharacterForFirstCreatingEnemies;
                        virusController.maxDistanceFromStartPoint = surfingControllerV2.dstTravelled + minFarFromCharacterForFirstCreatingEnemies;

                        virusController.minSpeedForRandom = surfingControllerV2.speed - maxSubtractSpeed;
                        virusController.maxSpeedForRandom = surfingControllerV2.speed - minSubtractSpeed;


                        virusController.minDistanceFromPathPoint = minDistanceFromPathPoint;
                        virusController.maxDistanceFromPathPoint = maxDistanceFromPathPoint;
                    }

                    firstEnemyCreatingControl = true;
                }
            }
            else
            {
                break;
            }
            
        }

    }
}
